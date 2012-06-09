using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectionStringManager
{
    public class SavedConnectionPresenter : GenericConnectionPresenter, IDisposable
    {
        protected override IGenericView View
        {
            get
            {
                return base.View;
            }
            set
            {
                base.View = value;
            }
        }

        public SavedConnectionPresenter(ISavedConnectionView view) : base(view)
        {
            View = view;

            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();

            ((ISavedConnectionView)View).UpdateSavedConnections += new UpdateConnectionAction(_view_UpdateSavedConnections);
        }

        void _view_UpdateSavedConnections(IList<SavedConnection> connections)
        {
            ProfileHelper ph = new ProfileHelper();
            ph.Replace(connections.ToList());

            View.RefreshView();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((ISavedConnectionView)View).UpdateSavedConnections -= _view_UpdateSavedConnections;
            }
            base.Dispose(disposing);
        }
    }
}
