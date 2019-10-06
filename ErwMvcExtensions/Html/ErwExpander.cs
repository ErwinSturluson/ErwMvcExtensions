using ErwMvcExtensions.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ErwMvcExtensions.Html
{
    public class ErwExpander : IDisposable
    {
        private readonly ViewContext viewContext;
        private bool disposed;

        public ErwExpander(ViewContext viewContext)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            this.viewContext = viewContext;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                this.disposed = true;
                HtmlHelperExpanderExtensions.EndExpander(this.viewContext);
            }
        }

        public void EndExpander()
        {
            this.Dispose(true);
        }
    }
}
