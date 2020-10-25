using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Poznamkovy_blok.Database;
using System.IO;

using SQLite;

namespace Poznamkovy_blok
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTask : ContentPage
    {
        string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3");

        Note task = new Note();
        public EditTask()
        {
            InitializeComponent();
            var db = new SQLiteConnection(_dbPath);
            listview.ItemsSource = db.Table<Note>().OrderBy(x => x.Name).ToList();
            

        }

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            task = (Note)e.SelectedItem;
            ID.Text = task.ID.ToString();
            TaskName.Text = task.Name;
            TaskText.Text = task.Text;
        }

        private async void OnUpdateTask (object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);

            DateTime date = DateTime.UtcNow;
            Note task = new Note()
            {
                ID = Convert.ToInt32(ID),
                Name = TaskName.Text.ToString(),
                Text = TaskText.Text.ToString(),
                Date_Last = date
            };
            db.Update(task);
            await DisplayAlert(null, task.Name + " Updated", "Ok");
            Navigation.InsertPageBefore(new HomePage(), this);
            await Navigation.PopAsync();
        }
    }
}