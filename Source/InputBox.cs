using System;
using System.Windows.Forms;

namespace NBody {

    partial class InputBox : Form {

        public InputBox() {
            InitializeComponent();
            Button button = new Button();
            button.Click += (sender, e) => {
                Close();
            };
            CancelButton = button;
        }

        void ButtonClick(Object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        public DialogResult ShowDialog(String message, String defaultInputText) {
            promptLabel.Text = message;
            responseBox.Text = defaultInputText;
            CenterToScreen();
            return ShowDialog();
        }

        public static String Show(String message, String defaultInputText = "") {
            using (InputBox box = new InputBox()) {
                if (box.ShowDialog(message, defaultInputText) == DialogResult.OK)
                    return box.responseBox.Text;
                return defaultInputText;
            }
        }
    }
}