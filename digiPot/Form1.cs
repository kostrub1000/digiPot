using System;
using System.IO.Ports;
using System.Runtime.InteropServices;

namespace digiPot
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SetCommBrake(IntPtr hFile);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool ClearCommBrake(IntPtr hFile);


        int Number_of_potentiometer { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        bool ports_are_closed = true;
        private void Open_COM_ports_button_Click(object sender, EventArgs e)
        {
            if (ports_are_closed)
            {
                try
                {
                    //открытие ком портов, получение указателей на них
                    serial_port_1.PortName = (available_COM_ports_comboBox1.Text);
                    serial_port_1.Open();                                              
                    FileStream file_stream_1 = (FileStream)serial_port_1.BaseStream;
                    serial_port_1_pointer = file_stream_1.SafeFileHandle.DangerousGetHandle();

                    serial_port_2.PortName = (available_COM_ports_comboBox2.Text);
                    serial_port_2.Open();
                    FileStream file_stream_2 = (FileStream)serial_port_2.BaseStream;
                    IntPtr serial_port_2_pointer = file_stream_2.SafeFileHandle.DangerousGetHandle();


                    set_resistance_button.Enabled = true;
                    up_button.Enabled = true;
                    down_button.Enabled = true;
                    Open_COM_ports_button.Text = "Закрыть COM порты";
                    ports_are_closed = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //закрытие ком портов
                serial_port_1.Close();
                serial_port_2.Close();
                serial_port_1_pointer = IntPtr.Zero;
                serial_port_2_pointer = IntPtr.Zero;
                set_resistance_button.Enabled = false;
                up_button.Enabled = false;
                down_button.Enabled = false;
                Open_COM_ports_button.Text = "Открыть COM порты";
                ports_are_closed = true;
            }


        }

        SerialPort serial_port_1;
        SerialPort serial_port_2;
        IntPtr serial_port_1_pointer = IntPtr.Zero;
        IntPtr serial_port_2_pointer = IntPtr.Zero;
        private void Form1_Load(object sender, EventArgs e)
        {
            //загрузка доступных ком портов и их настройка
            Number_of_potentiometer = (int)potentiometer_number_numericUpDown.Value;
            serial_port_1 = new SerialPort();
            serial_port_1.BaudRate = 9600;
            serial_port_1.DataBits = 8;
            serial_port_1.StopBits = StopBits.One;
            serial_port_1.Parity = Parity.None;
            

            serial_port_2 = new SerialPort();
            serial_port_2.BaudRate = 9600;
            serial_port_2.DataBits = 8;
            serial_port_2.StopBits = StopBits.One;
            serial_port_2.Parity = Parity.None;
            

            string[] ports;
            try
            {
                ports = SerialPort.GetPortNames();
                foreach (string s in ports)
                {
                    available_COM_ports_comboBox1.Items.Add(s);
                }

                available_COM_ports_comboBox1.SelectedIndex = 0;
                available_COM_ports_comboBox2.SelectedIndex = 1;
            }
            catch
            {
                MessageBox.Show("Должно быть как минимум два COM порта");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //закрытие открытых ком портов при закрытии формы
            if (serial_port_1.IsOpen)
            {
                serial_port_1.Close();
            }
            if (serial_port_2.IsOpen)
            {
                serial_port_2.Close();
            }
            serial_port_1_pointer = IntPtr.Zero;
            serial_port_2_pointer = IntPtr.Zero;
        }

        bool com_port_operation_in_progress = false;
        private async void up_button_Click(object sender, EventArgs e)
        {
            //действие при нажатии кнопни вверх
            if (!serial_port_1.IsOpen || !serial_port_2.IsOpen) { return; }     //проверка, открыты ли ком порты
            if (com_port_operation_in_progress) { return; }                     //проверка, закончена ли последняя операция с ком портами
            com_port_operation_in_progress = true;                              //начало следующей операции с ком портами
            Activate_nessesary_potentiometer();                                 //включение соответствующего потенциометра (сигнал на контакт cs)
            Set_to_increase_resistance();                                       //перестановка контакта U/D в положение повышения сопротивления
            await Send_pulse_signal(1);                                         //отправка сигнала на контакт INC 
            Deactivate_potentiometers();                                        //отключение всех контактов cs
        }

        private async void down_button_Click(object sender, EventArgs e)
        {
            if (!serial_port_1.IsOpen || !serial_port_2.IsOpen) { return; }     //проверка, открыты ли ком порты
            if (com_port_operation_in_progress) { return; }                     //проверка, закончена ли последняя операция с ком портами
            com_port_operation_in_progress = true;                              //начало следующей операции с ком портами
            Activate_nessesary_potentiometer();                                 //включение соответствующего потенциометра (сигнал на контакт cs)
            Set_to_decrease_resistance();                                       //перестановка контакта U/D в положение понижения сопротивления
            await Send_pulse_signal(1);                                         //отправка сигнала на контакт INC 
            Deactivate_potentiometers();                                        //отключение всех контактов cs
        }

        private async void set_resistance_button_Click(object sender, EventArgs e)
        {
            if (!serial_port_1.IsOpen || !serial_port_2.IsOpen) { return; }     //проверка, открыты ли ком порты
            if (com_port_operation_in_progress) { return; }                     //проверка, закончена ли последняя операция с ком портами
            com_port_operation_in_progress = true;                              //начало следующей операции с ком портами
            int resistance_to_set = Convert.ToInt32(resistance_textBox.Text);   //вычисление необходимого сопротивления
            Activate_nessesary_potentiometer();                                 //включение соответствующего потенциометра (сигнал на контакт cs)
            Set_to_decrease_resistance();                                       //перестановка контакта U/D в положение понижения сопротивления
            await Send_pulse_signal(100);                                       //отправка сигнала на контакт INC 
            Set_to_increase_resistance();                                       //перестановка контакта U/D в положение повышения сопротивления
            await Send_pulse_signal(resistance_to_set);                         //отправка сигнала на контакт INC 
            Deactivate_potentiometers();                                        //отключение всех контактов cs
        }


        void Activate_nessesary_potentiometer()
        {
            switch(Number_of_potentiometer)
            {
                case 1:
                    Activate_1_potentiometer();
                    break;
                case 2:
                    Activate_2_potentiometer();
                    break;
                case 3:
                    Activate_3_potentiometer();
                    break;
                case 4:
                    Activate_4_potentiometer();
                    break;
            }
        }

        void Activate_1_potentiometer()
        {
            serial_port_1.RtsEnable = true;
        }

        void Activate_2_potentiometer()
        {
            serial_port_2.RtsEnable = true ;
        }

        void Activate_3_potentiometer()
        {
            serial_port_1.DtrEnable = true ;
        }

        void Activate_4_potentiometer()
        {
            serial_port_2.DtrEnable = true ;
        }


        void Deactivate_potentiometers()
        {
            serial_port_1.RtsEnable = false;
            serial_port_2.RtsEnable = false;
            serial_port_1.DtrEnable = false;
            serial_port_2.DtrEnable = false;
            com_port_operation_in_progress = false;
        }

        void Set_to_increase_resistance()
        {
            SetCommBrake(serial_port_1_pointer);
        }

        void Set_to_decrease_resistance()
        {
            ClearCommBrake(serial_port_1_pointer);
        }

        private async Task Send_pulse_signal(int number)
        {
            
            for(int i = 0; i < number; i++)
            {
                SetCommBrake(serial_port_2_pointer);
                await Task.Delay(1);
                ClearCommBrake(serial_port_2_pointer);
                await Task.Delay(1);
            }
            
        }










        private void potentiometer_number_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Number_of_potentiometer = (int)potentiometer_number_numericUpDown.Value;
        }
    }



    public class Only_positive_int_less_than_100_allowed_Textbox : TextBox
    {
        public Only_positive_int_less_than_100_allowed_Textbox()
        {
            KeyPress += Only_int_allowed_Textbox_KeyPress;
            TextChanged += Only_int_allowed_Textbox_TextChanged;
        }

        char[] allowed_Characters = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public void Only_int_allowed_Textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!allowed_Characters.Contains(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool isUpdating = false;
        public void Only_int_allowed_Textbox_TextChanged(object sender, EventArgs e)
        {
            if (isUpdating) { return; }
            isUpdating = true;
            string help_string = this.Text;
            char[] text = help_string.ToCharArray();

            for (int i = 0; i < text.Length; i++)
            {
                if (!allowed_Characters.Contains(text[i]))
                {
                    help_string = "";
                    break;
                }
            }


            //text = help_string.ToCharArray();
            if(help_string == "")
            {
                help_string = "0";
            }
            if(Convert.ToInt32(help_string) > 100)
            {
                help_string = "100";
            }

            this.Text = help_string;

            isUpdating = false;
        }
    }
}
