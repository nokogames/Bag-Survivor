mergeInto(LibraryManager.library, {
    TriggerHapticFeedback: function(pattern) {
        // Tarayýcýnýn haptic desteði olup olmadýðýný kontrol et
        if (navigator.vibrate) {
            // Pattern array'ini al ve vibrate fonksiyonunu çaðýr
            navigator.vibrate(pattern);
        }
    }
});
