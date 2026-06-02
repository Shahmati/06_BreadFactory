using BreadFactory.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BreadFactory.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserEditWindow.xaml
    /// </summary>
    public partial class UserEditWindow : Window
    {
        private Users currentUser;

        public UserEditWindow(Users user)
        {
            InitializeComponent();

            if (user == null)
            {
                this.Title = "Хлебобулочный завод - добавление";
            }
            else this.Title = "Хлебобулочный завод - редактирование";

            cbRole.ItemsSource =
                DataBaseEntities
                .GetContext()
                .Roles
                .ToList();

            if (user == null)
            {
                currentUser = new Users();
            }
            else
            {
                currentUser = user;

                tbSecondName.Text =
                    currentUser.UserSecondName;

                tbFirstName.Text =
                    currentUser.UserFirstName;

                tbPatronymic.Text =
                    currentUser.UserPatronymic;

                tbLogin.Text =
                    currentUser.Login;

                pbPassword.Password =
                    currentUser.PasswordHash;

                chBlocked.IsChecked =
                    currentUser.IsBlocked;

                cbRole.SelectedValue =
                    currentUser.UserRoleKey;
            }
        }

        private void BtnSave_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSecondName.Text) ||
                string.IsNullOrWhiteSpace(tbFirstName.Text) ||
                string.IsNullOrWhiteSpace(tbLogin.Text) ||
                string.IsNullOrWhiteSpace(pbPassword.Password) ||
                cbRole.SelectedItem == null)
            {
                MessageBox.Show(
                    "Заполните обязательные поля",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            bool loginExists =
                DataBaseEntities
                .GetContext()
                .Users
                .Any(x =>
                    x.Login == tbLogin.Text &&
                    x.UserID != currentUser.UserID);

            if (loginExists)
            {
                MessageBox.Show(
                    "Пользователь с таким логином уже существует",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            currentUser.UserSecondName =
                tbSecondName.Text;

            currentUser.UserFirstName =
                tbFirstName.Text;

            currentUser.UserPatronymic =
                tbPatronymic.Text;

            currentUser.Login =
                tbLogin.Text;

            currentUser.PasswordHash =
                pbPassword.Password;

            currentUser.UserRoleKey =
                (int)cbRole.SelectedValue;

            currentUser.IsBlocked =
                chBlocked.IsChecked ?? false;

            if (currentUser.UserID == 0)
            {
                currentUser.CreatedAt =
                    System.DateTime.Now;

                DataBaseEntities
                    .GetContext()
                    .Users
                    .Add(currentUser);
            }

            DataBaseEntities
                .GetContext()
                .SaveChanges();

            MessageBox.Show(
                "Данные успешно сохранены",
                "Успех",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            DialogResult = true;

            Close();
        }
    }
}
