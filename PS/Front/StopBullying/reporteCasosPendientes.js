$(document).ready(function () {
    crearGrafico()
    crearGrafico2()
    // setChart();
});

let chart1;
let chart2;

function crearGrafico() {

    $.ajax({
        url: "https://localhost:5001/Denuncias/ObtenerDenunciasReportePendientes",
        type: "GET",
        //  contentType: "application/json",
        success: function (result) {
            if (result.ok) {
                var denuncia=result.return;
                console.log(denuncia);
                var rotulos = [];
                var datos = [];
                
                for (var i in denuncia) {
                     rotulos.push(denuncia[i].anio.year);
                     datos.push(denuncia[i].cantidad);
                }
                console.log(rotulos);
                console.log(datos);   
                
                const bgColor = {
                    id:'bgColor',
                    beforeDraw: (chart, options) => {
                        const { ctx, width, height } = chart
                        ctx.fillStyle = 'white'
                        ctx.fillRect(0, 0, width, height)
                        ctx.restore()
                    }
                }
                     

                const ctx = document.getElementById('totalAnual').getContext('2d');
                if(chart1){
                    chart1.destroy();
                  }

                chart1 = new Chart(ctx, {
                    type: 'bar',// 'doughnut',
                    data: {
                        labels: rotulos,
                        datasets: [{
                            label: 'Denuncias Pendientes por año',
                            data: datos,
                            borderColor: ['rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)'],
                            backgroundColor: ['rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)',
                            'rgba(140, 90, 100, 1)']

                        }]
                    },
                    options: {
                        scales: {
                            y: {
                            //    suggestedMax: 80,
                                beginAtZero: true
                            }
                        }
                    },
                    plugins:[bgColor]
                });
            } else {
                Swal.fire(result.error);
            }

        },
        error: function (error) {
            console.log(error);
        }
    });
}

function crearGrafico2() {

        $.ajax({
            url: "https://localhost:5001/Denuncias/ObtenerDenunciasReporteProgreso",
            type: "GET",
            //  contentType: "application/json",
            success: function (result) {
                if (result.ok) {
                    var denuncia=result.return;
                    console.log(denuncia);
                    var rotulos = [];
                    var datos = [];
                    
                    for (var i in denuncia) {
                         rotulos.push(denuncia[i].anio.year);
                         datos.push(denuncia[i].cantidad);
                    }
                    console.log(rotulos);
                    console.log(datos);        
                         
                    const bgColor = {
                        id:'bgColor',
                        beforeDraw: (chart, options) => {
                            const { ctx, width, height } = chart
                            ctx.fillStyle = 'white'
                            ctx.fillRect(0, 0, width, height)
                            ctx.restore()
                        }
                    }
    
                    const ctx = document.getElementById('totalElegir').getContext('2d');
                    if(chart2){
                        chart2.destroy();
                      }
    
                    chart2 = new Chart(ctx, {
                        type: 'bar',// 'doughnut',
                        data: {
                            labels: rotulos,
                            datasets: [{
                                label: 'Denuncias En progreso por año',
                                data: datos,
                                borderColor: ['rgba(255, 99, 132, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(255, 206, 86, 1)'],
                                backgroundColor: ['rgba(153, 102, 255, 1)',
                                'rgba(255, 159, 64, 1)',
                                'rgba(140, 90, 100, 1)']
    
                            }]
                        },
                        options: {
                            scales: {
                                y: {
                               //     suggestedMax: 20,
                                    beginAtZero: true
                                }
                            }
                        },
                        plugins:[bgColor]
                    });
                } else {
                    Swal.fire(result.error);
                }
    
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    let myChart;
    function grafico() {
        const now = new Date();
        const dia = `0${now.getDate()}`.slice(-2);
        const mes = `0${now.getMonth() + 1}`.slice(-2);
        const hoy = `${now.getFullYear()}-${mes}-${dia}`;
        let fechaDesde = $("#dtpFecDesde").val();
        let fechaHasta = $("#dtpFecHasta").val();
        if (fechaDesde == "") {
            Swal.fire("Ingrese Fecha Desde")
            return false;
           // fechaDesde = "1900-01-01";
        }
        if (fechaHasta == "") {
            Swal.fire("Ingrese Fecha Hasta")
            return false;
          //  fechaHasta = hoy;
        }
        if (fechaDesde > fechaHasta) {
            Swal.fire("Fechas Hasta, debe ser mayor a Fecha Desde")
            return false;
        }
        if (fechaDesde > hoy) {
            Swal.fire("Fecha Desde, debe ser menor al dia actual");
            return false;
        }
    
        if (fechaHasta > hoy) {
            Swal.fire("Fecha Hasta, debe ser como maximo el dia actual");
            return false;
        }
    
        $.ajax({
            url: 'https://localhost:5001/Reporte/totalEstado?param1=' + fechaDesde + '&param2=' + fechaHasta,
            type: "GET",
            success: function (result) {
                if (result.ok) {
                    let denuncia = result.return;
                    let rotulos = [];
                    let datos = [];
                    for (var i = 0; i < denuncia.length; i++) {
    
                        rotulos.push(denuncia[i].estados.estado1);
                        datos.push(denuncia[i].cantidad);
                    }
    
                    const bgColor = {
                        id: 'bgColor',
                        beforeDraw: (chart, options) => {
                            const { ctx, width, height } = chart
                            ctx.fillStyle = 'white'
                            ctx.fillRect(0, 0, width, height)
                            ctx.restore()
                        }
                    } 
    
                    const ctx = document.getElementById('myChart');
                    if (myChart) {
                        myChart.destroy();
                    }
                    myChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: rotulos,
                            datasets: [{
                                label: 'Denuncias segun Estado en el tiempo especificado',
                                data: datos,
                                backgroundColor: [
                                    'rgba(255, 99, 132, 1)',
                                    'rgba(54, 162, 235, 1)',
                                    'rgba(255, 206, 86, 1)',
                                    'rgba(75, 192, 192, 1)',
                                    'rgba(153, 102, 255, 1)',
                                    'rgba(255, 159, 64, 1)',
                                    'rgba(140, 90, 100, 1)'
                                ],
                                borderColor: [
                                    'rgba(255, 99, 132, 1)',
                                    'rgba(54, 162, 235, 1)',
                                    'rgba(255, 206, 86, 1)',
                                    'rgba(75, 192, 192, 1)',
                                    'rgba(153, 102, 255, 1)',
                                    'rgba(255, 159, 64, 1)',
                                    'rgba(140, 90, 100, 1)'
                                ],
                                borderWidth: 1
                            }]
                        },
                        options: {
                            
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        },
                        plugins: [bgColor]
                    });
    
                } else {
                    Swal.fire(result.error);
                }
    
            },
            error: function (error) {
                console.log(error);
            }
        });
    
    }