using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Poznamkovy_blok.Database;

namespace Poznamkovy_blok.Database {
    public class Database {
        readonly SQLiteAsyncConnection _DB;

        public Database(string dbPath) {
            _DB = new SQLiteAsyncConnection(dbPath);
            _DB.CreateTableAsync<Note>().Wait();
        }

        public async Task<List<Note>> GetNotesAsync() {
            return await _DB.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNoteAsync(int id) {
            return _DB.Table<Note>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<int> SaveNoteAsync(Note note) {
            int insertedRows = await _DB.InsertAsync(note);
            return insertedRows;
        }

        public async Task EditNoteAsync(int id, string label, string text) {
            Note note = await GetNoteAsync(id);

            note.Name = label;
            note.Text = text;
            note.Date_Creation = DateTime.UtcNow;
            note.Date_Last = DateTime.UtcNow;
            await _DB.UpdateAsync(note);
        }

        public async void DeleteNoteAsync(Note note) {
            int deletedRows = await _DB.DeleteAsync(note);
        }
    }
}