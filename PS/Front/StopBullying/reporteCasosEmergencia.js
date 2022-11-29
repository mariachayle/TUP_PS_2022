$(document).ready(function () {
    crearGrafico()
    crearGrafico2()
    // setChart();
});
/*
///no funciono
function getThis(){
    var resp=[];
    $.ajax({
        url: "Denuncias/ReporteEmergenciaEstado1",
        type: "GET",
        contentType: "application/json",
        success: function (data) {
            resp.push(data);
        },
        error: function(req, status, error){
            alert("error");
        }
});
return resp;
}
var simpledata=getThis();
var ctx=document.getElementById("totalAnual")
var barchart={
    labels: simpledata[0].mydate,
    datasets:[{
        label: "Pendiente",
        borderColor: "Tomato",
        backgroundColor: "Tomato",
        fill: false,
        data: simpledata[0].mySuccess1,
        yAxisID: 'y-axis-1'
    },{
        label: "En Progreso",
        borderColor: "MediumSeaGreen",
        backgroundColor: "MediumSeaGreen",
        fill: false,
        data: simpledata[0].mySuccess2,
        yAxisID: 'y-axis-2'
    },{
        label: "Resuelto",
        borderColor: "Green",
        backgroundColor: "Green",
        fill: false,
        data: simpledata[0].mySuccess3,
        yAxisID: 'y-axis-3'
    }]
};

window.myLine= Chart.Line(ctx, {
    data: barchart,
    options: {
        responsive:true,
        hoverMode: 'index',
        stacked: false,
        title: {
            display: true,
            text: "Denuncias de Emergencia"
        },
        scales: {
            yAxes: [{
                type:'linear',
                display: true,
                position: 'left',
                id: 'y-axis-1',
            },{
                type:'linear',
                display: true,
                position: 'right',
                id: 'y-axis-2',
            },{
                type:'linear',
                display: true,
                position: 'center',
                id: 'y-axis-3',

                gridLines:{
                    drawOnChartArea: false,
                },
            }],
               
            }
        }
    });


   // crearGraficoTotal();
    //crearGraficoEstado();
   // });
/*
   */
let chart1;
let chart2;

function crearGrafico() {

    $.ajax({
        url: "https://localhost:5001/Denuncias/ObtenerDenunciasReporteEmergencia",
        type: "GET",
        //  contentType: "application/json",
        success: function (result) {
            if (result.ok) {
                var denuncia = result.return;
                console.log(denuncia);
                var rotulos = [];
                var datos = [];

                for (var i in denuncia) {
                    rotulos.push(denuncia[i].anio.year);
                    datos.push(denuncia[i].cantidad);
                }
                console.log(rotulos);
                console.log(datos);
                /*
       $(denuncia).each(function (denuncia) {
             rotulos.push(denuncia.anio);
             datos.push(denuncia.cantidad);
         });
*/
                const bgColor = {
                    id:'bgColor',
                    beforeDraw: (chart1, options) => {
                        const { ctx, width, height } = chart1
                        ctx.fillStyle = 'white'
                        ctx.fillRect(0, 0, width, height)
                        ctx.restore()
                    }
                }

                const ctx = document.getElementById('totalAnual').getContext('2d');
                if (chart1) {
                    chart1.destroy();
                }

                chart1 = new Chart(ctx, {
                    type: 'bar',// 'doughnut',
                    data: {
                        labels: rotulos,
                        datasets: [{
                            label: 'Denuncias de Emergencia por año',
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
        url: "https://localhost:5001/Denuncias/ReporteEmergenciaEstado",
        type: "GET",
        //  contentType: "application/json",
        success: function (result) {
            if (result.ok) {
                var denuncia = result.return;
                console.log(denuncia);
                var rotulos = [];
                var datos = [];

                for (var i in denuncia) {
                    rotulos.push(denuncia[i].estados.estado1);
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
                if (chart2) {
                    chart2.destroy();
                }

                chart2 = new Chart(ctx, {
                    type: 'doughnut',// 'doughnut',
                    data: {
                        labels: rotulos,
                        datasets: [{
                            label: 'Denuncias de Emergencia segun Estado de este año',
                            data: datos,
                            borderColor: ['rgba(255, 99, 132, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(255, 206, 86, 1)'],
                            backgroundColor: ['rgba(153, 102, 255, 1)',
                                'rgba(255, 159, 64, 1)',
                                'rgba(140, 90, 100, 1)']

                        }]
                    },options:{
                        responsive:false
                    },
                    plugins:[bgColor]
                    /* options: {
                         scales: {
                             y: {
                                 suggestedMax: 50,
                                 beginAtZero: true
                             }
                         }
                     }*/
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
        Swal.fire("Fecha Hasta, debe ser mayor a Fecha Desde")
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
        url: 'https://localhost:5001/Reporte/totalEmergencia?param1=' + fechaDesde + '&param2=' + fechaHasta,
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
                            label: 'Denuncias de Emergencia segun Estado en el tiempo especificado',
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