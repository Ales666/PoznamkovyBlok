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
        string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3");

        public AddTask()
        {
            
            InitializeComponent();
        }

        private async void OnSaveTask (object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Note>();

            var Pk = db.Table<Note>().OrderByDescending(c => c.ID).FirstOrDefault();

            DateTime date = DateTime.UtcNow;
            Note task = new Note
            {
                ID = (Pk == null ? 1 : Pk.ID + 1),
                Name = TaskName.Text.ToString(),
                Text = TaskText.Text.ToString(),
                Date_Creation = date,
                Date_Last = date
            };
            db.Insert(task);
            await DisplayAlert(null, task.Name + " Saved", "Ok");
            Navigation.InsertPageBefore(new HomePage(), this);
            await Navigation.PopAsync();
        }
    }  
}