using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Frontend.Utils
{
    public class PopUpMessages
    {
          private readonly IJSRuntime _jsRuntime;

        public PopUpMessages(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ShowMessage(string message)
        {
            await _jsRuntime.InvokeVoidAsync("showToastMessage", message);
        }

        public async Task sweetAlert(string message,String title,string type)
        {
            await _jsRuntime.InvokeVoidAsync("show_sweet_alert",message,title,type);


            
        }
    }
}