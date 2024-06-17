using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CruzTareaMVVM.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CruzTareaMVVM.ViewModels
{
    internal class NotesViewModelsSC : IQueryAttributable
    {
        public ObservableCollection<ViewModels.NoteViewModelsSC> AllNotes { get; }
        public ICommand NewCommand { get; }
        public ICommand SelectNoteCommand { get; }

        public NotesViewModelsSC()
        {
            AllNotes = new ObservableCollection<ViewModels.NoteViewModelsSC>(Models.NoteSC.LoadAll().Select(n => new NoteViewModelsSC(n)));
            NewCommand = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommand = new AsyncRelayCommand<ViewModels.NoteViewModelsSC>(SelectNoteAsync);
        }

        private async Task NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.NotePageSC));
        }

        private async Task SelectNoteAsync(ViewModels.NoteViewModelsSC note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(Views.NotePageSC)}?load={note.Identifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                NoteViewModelsSC matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    AllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                NoteViewModelsSC matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
                }
                // If note isn't found, it's new; add it.
                else
                    AllNotes.Insert(0, new NoteViewModelsSC(Models.NoteSC.Load(noteId)));
            }
        }
    }
}
