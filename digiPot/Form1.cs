using System;
using System.IO.Ports;

namespace digiPot
{
    public partial class Form1 : Form
    {
        int Number_of_potentiometer { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        bool port_is_closed = true;
        private void Open_COM_port_button_Click(object sender, EventArgs e)
        {
            if (port_is_closed)
            {
                try
                {
                    serial_port.PortName = (available_COM_ports_comboBox.Text);
                    serial_port.Open();
                    set_resistance_button.Enabled = true;
                    up_button.Enabled = true;
                    down_button.Enabled = true;
                    Open_COM_port_button.Text = "Закрыть COM порт";
                    port_is_closed = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                serial_port.Close();
                set_resistance_button.Enabled = false;
                up_button.Enabled = false;
                down_button.Enabled = false;
                Open_COM_port_button.Text = "Открыть COM порт";
                port_is_closed = true;
            }


        }

        SerialPort serial_port;
        private void Form1_Load(object sender, EventArgs e)
        {
            Number_of_potentiometer = (int)potentiometer_number_numericUpDown.Value;
            serial_port = new SerialPort();
            serial_port.BaudRate = 9600;
            serial_port.DataBits = 8;
            serial_port.StopBits = StopBits.One;
            serial_port.Parity = Parity.None;

            string[] ports;
            try
            {
                ports = SerialPort.GetPortNames();
                foreach (string s in ports)
                {
                    available_COM_ports_comboBox.Items.Add(s);
                }

                available_COM_ports_comboBox.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("Нет ни одного COM порта");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serial_port.IsOpen)
            {
                serial_port.Close();
            }
        }

        private void up_button_Click(object sender, EventArgs e)
        {
            Activate_nessesary_potentiometer();
            Set_to_increase_resistance();
            Send_pulse_signal(1);
            
        }

        private void down_button_Click(object sender, EventArgs e)
        {
            Activate_nessesary_potentiometer();
            Set_to_decrease_resistance();
            Send_pulse_signal(1);
        }

        private void set_resistance_button_Click(object sender, EventArgs e)
        {
            int resistance_to_set = Convert.ToInt32(resistance_textBox.Text);
            Activate_nessesary_potentiometer();
            Set_to_decrease_resistance();
            Send_pulse_signal(100);
            Set_to_increase_resistance();
            Send_pulse_signal(resistance_to_set);
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
            if (!serial_port.IsOpen) { return; }

        }

        void Activate_2_potentiometer()
        {
            if (!serial_port.IsOpen) { return; }

        }

        void Activate_3_potentiometer()
        {
            if (!serial_port.IsOpen) { return; }

        }

        void Activate_4_potentiometer()
        {
            if (!serial_port.IsOpen) { return; }

        }

        void Set_to_increase_resistance()
        {
            if (!serial_port.IsOpen) { return; }

        }

        void Set_to_decrease_resistance()
        {
            if (!serial_port.IsOpen) { return; }

        }

        private async void Send_pulse_signal(int number)
        {
            if (!serial_port.IsOpen) { return; }
            byte[] message = new byte[1];
            for(int i = 0; i < number; i++)
            {
                serial_port.Write(message, 0, message.Length);
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
