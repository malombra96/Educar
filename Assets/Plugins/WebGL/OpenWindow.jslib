var OpenWindowPlugin = {
    openWindow: function(link)
    {
        var url_1 = Pointer_stringify(link);
        document.onmouseup = function()
        {
            //document.write(url)
            //window.open(url);
         
        }
        
        var element = document.createElement('a');
        element.setAttribute('href',url_1);
        element.setAttribute('download', "Desempeno.png");
        
            element.style.display = 'none';
            document.body.appendChild(element);
        
            element.click();
        
            
    }
};

mergeInto(LibraryManager.library, OpenWindowPlugin);