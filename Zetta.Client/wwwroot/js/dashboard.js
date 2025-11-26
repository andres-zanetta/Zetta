window.zettiumDashboard = window.zettiumDashboard || {};

// --- GRÁFICO DE PRESUPUESTOS ---
window.zettiumDashboard.initPresupuestosChart = function (stats) {
    if (!stats) return;
    const canvas = document.getElementById("presupuestosChart");
    if (!canvas) return;

    if (canvas._chartInstance) {
        canvas._chartInstance.destroy();
    }

    const ctx = canvas.getContext("2d");

    const data = {
        labels: ["Aceptados", "Pendientes", "Vencidos"],
        datasets: [{
            label: "Cantidad",
            data: [
                stats.presupuestosAceptados || 0,
                stats.presupuestosPendientes || 0,
                stats.presupuestosVencidos || 0
            ],
            backgroundColor: [
                "rgba(40, 167, 69, 0.8)",   // Verde
                "rgba(255, 193, 7, 0.8)",   // Amarillo
                "rgba(220, 53, 69, 0.8)"    // Rojo
            ],
            borderColor: [
                "rgba(40, 167, 69, 1)",
                "rgba(255, 193, 7, 1)",
                "rgba(220, 53, 69, 1)"
            ],
            borderWidth: 1
        }]
    };

    const config = getChartConfig(data);

    canvas._chartInstance = new Chart(ctx, config);
};

// --- GRÁFICO DE OBRAS (NUEVO) ---
window.zettiumDashboard.initObrasChart = function (stats) {
    if (!stats) return;
    const canvas = document.getElementById("obrasChart");
    if (!canvas) return;

    if (canvas._chartInstance) {
        canvas._chartInstance.destroy();
    }

    const ctx = canvas.getContext("2d");

    const data = {
        labels: ["Iniciadas", "En Proceso", "Finalizadas"],
        datasets: [{
            label: "Cantidad",
            data: [
                stats.obrasIniciadas || 0,
                stats.obrasProceso || 0,
                stats.obrasFinalizadas || 0
            ],
            backgroundColor: [
                "rgba(13, 202, 240, 0.8)",  // Cyan (Info)
                "rgba(253, 126, 20, 0.8)",  // Naranja (En proceso)
                "rgba(25, 135, 84, 0.8)"    // Verde (Finalizada)
            ],
            borderColor: [
                "rgba(13, 202, 240, 1)",
                "rgba(253, 126, 20, 1)",
                "rgba(25, 135, 84, 1)"
            ],
            borderWidth: 1
        }]
    };

    const config = getChartConfig(data);

    canvas._chartInstance = new Chart(ctx, config);
};

// Función auxiliar para no repetir la configuración visual
function getChartConfig(data) {
    return {
        type: "bar",
        data: data,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false },
                tooltip: {
                    titleColor: '#fff',
                    bodyColor: '#fff',
                    backgroundColor: 'rgba(0,0,0,0.9)',
                    borderColor: 'orange',
                    borderWidth: 1
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: { color: '#e0e0e0', stepSize: 1 },
                    grid: { color: 'rgba(255, 255, 255, 0.1)' },
                    border: { color: '#e0e0e0' }
                },
                x: {
                    ticks: { color: '#e0e0e0', font: { weight: 'bold' } },
                    grid: { display: false },
                    border: { color: '#e0e0e0' }
                }
            }
        }
    };
}