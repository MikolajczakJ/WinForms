using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList
{
    //Jestem na 6 stronie pdf, anchor komponentów zrobiony
    public partial class Form1 : Form
    {
        private BindingList<ToDoEntry> entries = new BindingList<ToDoEntry>();
        public Form1()
        {
            InitializeComponent();
            titleText.DataBindings.Add("Text",entriesSource,"Title",true,
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
                    UpdateListViewItem(e.NewIndex);
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
            item.SubItems[1].Text =entry.DueTime.ToShortDateString();
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
    }
}
