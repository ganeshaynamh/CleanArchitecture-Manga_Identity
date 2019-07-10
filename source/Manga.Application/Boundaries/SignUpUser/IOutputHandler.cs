using System;
using System.Collections.Generic;
using System.Text;

namespace Manga.Application.Boundaries.SignUpUser
{
    public interface IOutputHandler : IErrorHandler
    {
        void Handle(Output output);
    }
}
