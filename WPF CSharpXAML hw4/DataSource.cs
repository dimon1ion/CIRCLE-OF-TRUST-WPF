using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace WPF_CSharpXAML_hw4
{
    class DataSource : INotifyPropertyChanged
    {
        private string starEnable = AppDomain.CurrentDomain.BaseDirectory + "\\StarEnable.png";
        public string StarEnable => starEnable;

        private string name;
        private int selectedIndex;
        ICommand addCommand;
        ICommand deleteCommand;
        ICommand saveCommand;
        ICommand loadCommand;

        public ObservableCollection<User> Users { get; set; }
        public DataSource()
        {
            selectedIndex = -1;
            name = String.Empty;
            Users = new ObservableCollection<User>();
            addCommand = new Command(() =>
            {
                Users.Add(new User(name));
                Name = String.Empty;
                SelectedIndex++;
            });
            deleteCommand = new Command(() =>
            {
                SelectedIndex--;
                Users.RemoveAt(SelectedIndex + 1);
            }, () =>
            {
                return selectedIndex >= 0;
            });
            saveCommand = new Command(() =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Json(*.json)|*.json";
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<User>));
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                        {
                            jsonSerializer.WriteObject(fs, Users);
                        }
                    }
                    catch (Exception) { }
                }
            });
            loadCommand = new Command(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Json(*.json)|*.json";
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<User>));
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                        {
                            ObservableCollection<User> users = new ObservableCollection<User>();
                            users = (ObservableCollection<User>)jsonSerializer.ReadObject(fs);
                            Users.Clear();
                            foreach (User user in users)
                            {
                                SelectedIndex = 0;
                                CheckDelete();
                                Users.Add(user);
                            }
                        }
                    }
                    catch (Exception) { }
                }
            });
        }
        public string Name
        {
            get => name;
            set
            {
                if (!name.Equals(value))
                {
                    name = value;
                    PropChanged(nameof(Name));
                }
            }
        }
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (!selectedIndex.Equals(value))
                {
                    selectedIndex = value;
                    CheckDelete();
                    PropChanged(nameof(SelectedIndex));
                }
            }
        }

        public ICommand AddCommand => addCommand;
        public ICommand DeleteCommand => deleteCommand;
        public ICommand SaveCommand => saveCommand;
        public ICommand LoadCommand => loadCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        private void CheckDelete()
        {
            (deleteCommand as Command).Check();
        }

        private void PropChanged(string nameProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameProp));
        }
    }
}
