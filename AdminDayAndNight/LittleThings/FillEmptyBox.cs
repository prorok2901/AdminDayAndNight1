using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AdminDayAndNight.LittleThings
{
    internal class FillEmptyBox
    {
        public static void FillEmptyTextBox(string _text, TextBox _box)
        {
            if(_box.Text == "")
            {
                _box.Text = _text;
            }
            else if(_box.Text == _text)
            {
                _box.Text = "";
            }
        }
        public static void FillEmptyPassword(PasswordBox _password, TextBox _textBox, Image _inkognito, BitmapImage _bitmap, int _opacity, bool _isEnabled)
        {
            _inkognito.Source = _bitmap;

            _password.Opacity = _opacity;

            if (_password.Password != "")
            {
                _textBox.Text = _password.Password;
            }
            else
            {
                _textBox.Text = "Введите пароль";
            }
            _password.IsEnabled = _isEnabled;
        }
    }
}
