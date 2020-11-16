// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getData(){
    return Math.random();
}

Plotly.plot('chart', [{
    y:[getData()],
    type: 'line'
}]);

var cnt = 0;

setInterval(function (){

    Plotly.extendTraces('chart', {y:[[getData()]]}, [0]);
    cnt++;

    if (cnt>100){
        Plotly.relayout('chart', {
            xaxis:{
                range:[cnt-100, cnt]
            }
        });
    }
}, 15);
