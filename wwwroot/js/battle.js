$(document).ready(function () {
    // Получение данных боя из контроллера
    $.ajax({
        url: window.location.href,
        method: 'GET',
        success: function (data) {
            var battleData = data;
            var index = 0;

            function displayNext() {
                if (index < battleData.length) {
                    var event = battleData[index];
                    var message = "";

                    if (event.Message) {
                        message = `<p><strong>${event.Message}</strong></p>`;
                    } else {
                        message = `<p>Раунд ${event.Round}: ${event.Attacker} наносит ${event.Damage} урона ${event.Defender}. Здоровье ${event.Defender}: ${event.DefenderHealth}</p>`;
                    }

                    $('#battle-log').append(message);

                    index++;
                    setTimeout(displayNext, 1000); // Пауза между событиями
                }
            }

            displayNext();
        },
        error: function () {
            $('#battle-log').append('<p>Произошла ошибка при получении данных боя.</p>');
        }
    });
});
 