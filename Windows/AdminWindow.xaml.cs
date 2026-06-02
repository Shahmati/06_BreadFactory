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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            LoadUsers();
        }

        private void LoadUsers()
        {
            dgUsers.ItemsSource =
                DataBaseEntities
                .GetContext()
                .Users
                .ToList();
        }

        private void btnRefresh_Click(
            object sender,
            RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void btnUnblock_Click(
            object sender,
            RoutedEventArgs e)
        {
            Users selectedUser =
                dgUsers.SelectedItem as Users;

            if (selectedUser == null)
            {
                MessageBox.Show(
                    "Выберите пользователя",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            selectedUser.IsBlocked = false;

            selectedUser.FailedAttempts = 0;

            DataBaseEntities
                .GetContext()
                .SaveChanges();

            MessageBox.Show(
                "Пользователь успешно разблокирован",
                "Успех",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            LoadUsers();
        }

        private void btnAdd_Click(
            object sender,
            RoutedEventArgs e)
        {
            UserEditWindow window =
                new UserEditWindow(null);

            window.ShowDialog();

            LoadUsers();
        }

        private void btnEdit_Click(
            object sender,
            RoutedEventArgs e)
        {
            Users selectedUser =
                dgUsers.SelectedItem as Users;

            if (selectedUser == null)
            {
                MessageBox.Show(
                    "Выберите пользователя",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            UserEditWindow window =
                new UserEditWindow(selectedUser);

            window.ShowDialog();

            LoadUsers();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Users selectedUser = dgUsers.SelectedItem as Users;
            if (selectedUser == null)
            {
                MessageBox.Show(
                    "Выберите пользователя",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }
            
        }
    }
}
