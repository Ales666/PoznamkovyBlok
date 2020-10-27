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
    public partial class HomePage : ContentPage
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3");

        public HomePage()
        {
            InitializeComponent();

            var db = new SQLiteConnection(dbPath);
            listview.ItemsSource = db.Table<Note>().OrderBy(x => x.Name).ToList();
        }

        //presmerovani na AddTask, kde probiha add tasku
        public void OnAddTask(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AddTask());
        }

        //presmerovani na EditTask, kde probiha edit tasku
        public void OnEditTask(object sender, EventArgs args)
        {
            Navigation.PushAsync(new EditTask());
        }

        //mazani tasku
        private async void OnDeleteTask(object sender, EventArgs args)
        {
            Button clickedButton = (Button)sender;
            string ID = clickedButton.ClassId.ToString();

            Note note = await App.Database.GetNoteAsync(int.Parse(ID));
            App.Database.DeleteNoteAsync(note);

            //po uspesnem smazani vyskoci okno, ktere sdeli uzivateli, ze task byl vymazan
            await DisplayAlert(null, "Task was deleted", "Ok");
            //reload hlavni stranky, aby byly videt zmeny
            Navigation.InsertPageBefore(new HomePage(), this);
            await Navigation.PopAsync();
        }
    }
}