using System;
using System.Collections.Generic;
using System.Text;

namespace Manga.Application.Boundaries.LoginUser
{
    public interface IOutputHandler : IErrorHandler
    {
        void Handle(Output output);
    }
}
