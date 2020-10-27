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
    public partial class AddTask : ContentPage
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3");

        public AddTask()
        {
            InitializeComponent();
        }

        //po kliknutí na tlačítko se spustí funkce, která uloží inputy do databáze
        private async void OnSaveTask (object sender, EventArgs e)
        {
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<Note>();

            var XXX = db.Table<Note>().OrderByDescending(c => c.ID).FirstOrDefault();

            //do promenne date se uklada aktualni datum
            DateTime date = DateTime.UtcNow;

            //data z inputu a datum se uklada do databaze
            Note task = new Note
            {
                ID = (XXX == null ? 1 : XXX.ID + 1),
                Name = TaskName.Text.ToString(),
                Text = TaskText.Text.ToString(),
                Date_Creation = date,
                Date_Last = date
            };
            //ulozeni do databaze
            db.Insert(task);
            //po uspesnem ulozeni vyskoci okno, ktere sdeli uzivateli, ze task byl ulozen
            await DisplayAlert(null, task.Name + " Saved", "Ok");
            //nasmerovani zpatky na domovskou obrazovku
            Navigation.InsertPageBefore(new HomePage(), this);
            await Navigation.PopAsync();
        }
    }  
}