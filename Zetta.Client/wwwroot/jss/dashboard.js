window.zettiumDashboard = window.zettiumDashboard || {};

window.zettiumDashboard.initPresupuestosChart = function (stats) {
    if (!stats) return;

    const canvas = document.getElementById("presupuestosChart");
    if (!canvas) return;

    const ctx = canvas.getContext("2d");

    if (canvas._chartInstance) {
        canvas._chartInstance.destroy();
    }

    const data = {
        labels: ["Aceptados", "Pendientes", "Vencidos"],
        datasets: [{
            label: "Cantidad de presupuestos",
            data: [
                stats.presupuestosAceptados || 0,
                stats.presupuestosPendientes || 0,
                stats.presupuestosVencidos || 0
            ],
            backgroundColor: [
                "rgba(0, 200, 0, 0.7)",
                "rgba(255, 165, 0, 0.7)",
                "rgba(200, 0, 0, 0.7)"
            ]
        }]
    };

    const chart = new Chart(ctx, {
        type: "bar",
        data: data,
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true,
                    precision: 0
                }
            }
        }
    });

    canvas._chartInstance = chart;
};
