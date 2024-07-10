using Question2.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Question2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Prn212TrialContext context = new Prn212TrialContext();
        public MainWindow()
        {
            InitializeComponent();
            loadEmployees();
        }

        private void loadEmployees()
        {
            lvEmployees.ItemsSource = context.Employees.ToList();
        }

        private void HandleBeforeLoaded()
        {
            loadEmployees();
        }

        public Employee GetEmployeeObject()
        {
            try
            {

                Employee employee = new Employee
                {
                    Id = string.IsNullOrEmpty(employeeId.Text) ? 0 : int.Parse(employeeId.Text),
                    Name = employeeName.Text,
                    Gender = Male.IsChecked == true ? "Male" : "Female",
                    Phone = txtPhone.Text,
                    Dob = dob.SelectedDate.HasValue ? DateOnly.FromDateTime(dob.SelectedDate.Value) : (DateOnly?)null,
                    Idnumber = txtIdNumber.Text,
                };
                return employee;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot get employee", "Get Employee");
                return null;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            employeeId.Text = string.Empty;
            employeeName.Text = string.Empty;
            Male.IsChecked = true;
            txtPhone.Text = string.Empty;
            dob.SelectedDate = null;
            txtIdNumber.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employee = GetEmployeeObject();
                if (employee != null)
                {
                    employee.Id = 0;
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    loadEmployees();
                    MessageBox.Show("Insert employee success", "Add Employee");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert employee failed", "Add Employee");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employee = GetEmployeeObject();
                if (employee != null)
                {
                    var oldEmployee = context.Employees.FirstOrDefault(x => x.Id == employee.Id);
                    if(oldEmployee != null)
                    {
                        oldEmployee.Dob = employee.Dob;
                        oldEmployee.Name = employee.Name;
                        oldEmployee.Phone = employee.Phone;
                        oldEmployee.Gender = employee.Gender;
                        oldEmployee.Idnumber = employee.Idnumber;
                        context.Employees.Update(oldEmployee);
                        context.SaveChanges();
                        loadEmployees();
                        MessageBox.Show("Edit employee success", "Edit Employee");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Edit employee failed", "Edit Employee");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employee = GetEmployeeObject();
                if (employee != null)
                {
                    var oldEmployee = context.Employees.FirstOrDefault(x => x.Id == employee.Id);
                    if (oldEmployee != null)
                    {
                        context.Employees.Remove(oldEmployee);
                        context.SaveChanges();
                        loadEmployees();
                        MessageBox.Show("Delete employee success", "Delete Employee");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete employee failed", "Delete Employee");
            }
        }
        private void listEmployee_click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var gender = ((Employee)item).Gender;
                if (!string.IsNullOrEmpty(gender))
                {
                    if (gender.ToLower() == "female")
                    {
                        Female.IsChecked = true;
                    }
                    else
                    {
                        Male.IsChecked = true;
                    }
                }
            }
        }
    }
}