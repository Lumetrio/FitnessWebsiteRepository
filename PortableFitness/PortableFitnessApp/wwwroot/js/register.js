document.addEventListener('DOMContentLoaded', function () {
    const activityLevelInput = document.getElementById('ActivityLevel');
    const activityLevelDescription = document.getElementById('activity-level-description');

    function updateActivityDescription() {
        const value = activityLevelInput.value;
        let description;

        switch (parseInt(value)) {
            case 0:
                description = 'Крайне редко выхожу из дома';
                break;
            case 1:
                description = 'Иногда выхожу на прогулку.';
                break;
            case 2:
                description = 'Регулярные прогулки.Спорт пару раз в неделю';
                break;
            case 3:
                description = 'Спорт каждый день.';
                break;
            default:
                description = 'Не активен';
        }

        activityLevelDescription.textContent = description;
    }

    activityLevelInput.addEventListener('input', updateActivityDescription);
    updateActivityDescription(); 
});