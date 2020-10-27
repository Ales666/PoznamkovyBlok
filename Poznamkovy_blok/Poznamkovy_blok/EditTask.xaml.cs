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
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3");
        Note task = new Note();

        public EditTask()
        {
            InitializeComponent();

            var db = new SQLiteConnection(dbPath);
            listview.ItemsSource = db.Table<Note>().OrderBy(x => x.Name).ToList();
        }

        //jakmile uzivatel klikne na task, podle jeho dat se zmeni inputy, aby se mohlo editovat
        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            task = (Note)e.SelectedItem;
            ID.Text = task.ID.ToString();
            TaskName.Text = task.Name;
            TaskText.Text = task.Text;
        }

        //edit tasku
        private async void OnUpdateTask (object sender, EventArgs e)
        {
            var db = new SQLiteConnection(dbPath);

            DateTime date = DateTime.UtcNow;
            Note task = new Note()
            {
                ID = Convert.ToInt32(ID.Text.ToString()),
                Name = TaskName.Text.ToString(),
                Text = TaskText.Text.ToString(),
                Date_Last = date
            };
            db.Update(task);

            //po uspesnem editu vyskoci okno, ktere sdeli uzivateli, ze task byl upraven
            await DisplayAlert(null, task.Name + " Updated", "Ok");
            //nasmerovani zpatky na domovskou obrazovku
            Navigation.InsertPageBefore(new HomePage(), this);
            await Navigation.PopAsync();
        }
    }
}