using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ToDoList
{
    //Jestem na 6 stronie pdf, anchor komponentów zrobiony
    public partial class Form1 : Form
    {
        private BindingList<ToDoEntry> entries = new BindingList<ToDoEntry>();
        private SaveFileDialog saveFileDialog = new SaveFileDialog();
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        public Form1()
        {
            InitializeComponent();
            titleText.DataBindings.Add("Text", entriesSource, "Title", true,
                DataSourceUpdateMode.OnPropertyChanged);
            dueDatePicker.DataBindings.Add("Value", entriesSource, "DueTime", true,
                DataSourceUpdateMode.OnPropertyChanged);
            entriesSource.DataSource = entries;
            CreateNewItem();
        }
        private void CreateNewItem()
        {
            ToDoEntry newEntry = (ToDoEntry)entriesSource.AddNew();
            newEntry.Title = "Nowe zadania";
            newEntry.DueTime = DateTime.Now;
            entriesSource.ResetCurrentItem();
        }
        private void dueDatePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void entriesSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    MakeListViewItemForNewEntry(e.NewIndex);
                    break;
                case ListChangedType.ItemDeleted:
                    RemoveListViewItem(e.NewIndex);
                    break;
                case ListChangedType.ItemChanged:
                    UpdateListViewItem((e.NewIndex));
                    break;
                default:
                    break;
            }
        }
        private void MakeListViewItemForNewEntry(int newItemIndex)
        {
            ListViewItem item = new ListViewItem();
            item.SubItems.Add("");
            entriesListView.Items.Insert(newItemIndex, item);
        }
        private void UpdateListViewItem(int itemIndex)
        {
            ListViewItem item = entriesListView.Items[itemIndex];
            ToDoEntry entry = entries[itemIndex];
            item.SubItems[0].Text = entry.Title;
            item.SubItems[1].Text = entry.DueTime.ToShortDateString();
        }
        private void RemoveListViewItem(int deletedItemIndex)
        {
            entriesListView.Items.RemoveAt(deletedItemIndex);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (entriesListView.SelectedIndices.Count != 0)
            {
                int entryIndex = entriesListView.SelectedIndices[0];
                entriesSource.RemoveAt(entryIndex);
            }
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            CreateNewItem();
        }

        private void entriesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (entriesListView.SelectedIndices.Count != 0)
            {
                int entryIndex = entriesListView.SelectedIndices[0];
                entriesSource.Position = entryIndex;
            }
        }

        private void SaveList_Click(object sender, EventArgs e)
        {
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var toSave = new List<ToDoEntry>(entries);
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    var serializer = new XmlSerializer(typeof(List<ToDoEntry>));
                    serializer.Serialize(writer, toSave);
                }
            }
        }

        private void LoadList_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.Title = "Open ToDo List";

            var loadedList = new BindingList<ToDoEntry>();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var reader = new StreamReader(openFileDialog.FileName))
                {
                    var serializer = new XmlSerializer(typeof(List<ToDoEntry>));
                    var listFromXml = (List<ToDoEntry>)serializer.Deserialize(reader);
                    foreach (var item in listFromXml)
                    {
                        ToDoEntry newEntry = (ToDoEntry)entriesSource.AddNew();
                        newEntry.Title = item.Title;
                        newEntry.DueTime = item.DueTime;
                        newEntry.Description = item.Description;
                        entriesSource.ResetCurrentItem();
                    }
                }
            }
        }
    }

}