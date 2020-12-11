using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BoundyBotNet.Services
{
    public class CommandService
    {
        private FileService _fileService;

        public CommandService()
        {
            _fileService = new FileService();
        }

        public Dictionary<string, Stream> BuildCommandList()
        {
            var commandList = new Dictionary<string, Stream>();

            return commandList;
        }


    }
}
