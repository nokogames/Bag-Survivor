mergeInto(LibraryManager.library, {
    TriggerHapticFeedback: function(pattern) {
        // Taray�c�n�n haptic deste�i olup olmad���n� kontrol et
        if (navigator.vibrate) {
            // Pattern array'ini al ve vibrate fonksiyonunu �a��r
            navigator.vibrate(pattern);
        }
    }
});
