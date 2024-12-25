// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// وظيفة لتبديل الوضع الداكن
const toggleDarkMode = () => {
    document.body.classList.toggle('dark-mode');
    document.querySelectorAll('.stats-card').forEach(card => card.classList.toggle('dark-mode'));
};

// إذا كنت ترغب في إضافة زر لتبديل الوضع الداكن، يمكنك استخدام الكود التالي:
const darkModeButton = document.getElementById('dark-mode-button');
if (darkModeButton) {
    darkModeButton.addEventListener('click', toggleDarkMode);
}
