using System;

namespace ConnectionStringManager
{
    public class GenericConnectionPresenter : IDisposable
    {
        protected virtual IGenericView View { get; set; }
        protected virtual ManagerModel Model { get; set; } // =  { get; set; }

        protected GenericConnectionPresenter() { }

        public GenericConnectionPresenter(IGenericView view)
        {
            View = view;
            Model = ManagerModel.Instance;

            Initialize();
        }

        protected virtual void Initialize()
        {
            View.TestConnection += new ConnectionAction(_view_TestConnection);
            View.ConnectionStrings = Model.Entries;

            ProfileHelper ph = new ProfileHelper();
            View.SavedConnections = ph.SavedConnections;
        }

        protected virtual void _view_TestConnection(ConnectionStringEntry entry, bool silent)
        {
            var result = ConnectionHelper.ValidateConnection(entry);

            if (result) { View.SetMessage("Success!"); }
            else { View.SetError("Failed!"); }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                View.TestConnection -= _view_TestConnection;
            }
        }
    }
}
