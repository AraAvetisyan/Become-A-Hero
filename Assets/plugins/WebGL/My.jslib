mergeInto(LibraryManager.library, {
    
     IsMobile: function()
     {
         return Module.SystemInfo.mobile;
     },
 
 
    ShowAdd: function () {
                
        ysdk.adv.showFullscreenAdv({
            
            callbacks: {
                 onOpen: () => {
                   myGameInstance.SendMessage('GameManager', 'Stop', '1');
                   console.log("game stoped");
                },
               
                onClose: function(wasShown) {

                    myGameInstance.SendMessage('GameManager', 'Stop', '2');
                    console.log("game continue");
                },
                onError: function(error) {

                }
            }
        })
    },

 

});