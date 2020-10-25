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
        string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3");

        public HomePage()
        {
            InitializeComponent();

            var db = new SQLiteConnection(_dbPath);

            listview.ItemsSource = db.Table<Note>().OrderBy(x => x.Name).ToList();
        }

        public void OnAddTask(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AddTask());
        }

        public void OnEditTask(object sender, EventArgs args)
        {
            Navigation.PushAsync(new EditTask());
        }

        public async void OnDeleteTask(object sender, EventArgs args)
        {
            Button clickedButton = (Button)sender;
            string ID = clickedButton.ClassId.ToString();

            Note note = await App.Database.GetNoteAsync(int.Parse(ID));
            App.Database.DeleteNoteAsync(note);
            Refresh();
        }

        public void Refresh()
        {
            Navigation.InsertPageBefore(new HomePage(), this);
            Navigation.PopAsync();
        }
    }
}