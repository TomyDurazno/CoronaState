//READY
$(onReady);

function onReady() {
    var pies = $('[data-type="pie"]');

    pies.map((i, d) => makeOptions(d))
        .map((i, d) => makePieChart(d));

    var lines = $('[data-type="line"]');

    lines.map((i, d) => makeOptions(d))
        .map((i, d) => makeLineChart(d));

    var bars = $('[data-type="bar"]');

    bars.map((i, d) => makeOptions(d))
        .map((i, d) => makeBarChart(d));

    var polars = $('[data-type="polarArea"]');

    polars.map((i, d) => makeOptions(d))
          .map((i, d) => makePolarArea(d));

    var tabbuttons = $('[data-tabpanel]');

    tabbuttons.map((i, tab) => $(tab).on("click", function () { tabButtonOnClick(tab, tabbuttons) }));
}

function tabButtonOnClick(tab, tabbuttons) {

    var others = tabbuttons.filter((i, t) => t.id !== tab.id);

    others.map((i, t) => $(t).removeClass("active"));

    $(tab).addClass("active");

    var continent = $(tab).data("continent");

    var otherpanels = $(".panel").filter((i, p) => p.id !== tab.dataset.tabpanel);

    otherpanels.map((i, p) => $(p).hide());

    var myPanel = $(".panel").filter((i, p) => p.id === tab.dataset.tabpanel);

    myPanel.map((i, p) => $(p).attr("hidden", false).show());  

    store.rebuildFromState(continent, () => $("#tab-content-container"));
}

function makeOptions(d) {
    return {
        name: d.dataset.name,
        type: d.dataset.type,
        id: d.id,
        url: d.dataset.url,
        data: {
            continent: d.dataset.continent
        }
    }
}

function makeChart(options, obj) {
    if (window[options.name]) {
        window[options.name].destroy();
    }

    window[options.name] = new Chart(document.getElementById(options.id).getContext("2d"), obj);
}

function makePieChart(options) {

    function onSuccess(data) {

        obj = {
            type: options.type,
            data: data,
            options: {
                responsive: !0
            }
        };

        makeChart(options, obj);
    }

    store.call(options, onSuccess);
}

function makeBarChart(options) {

    function onSuccess(data) {

        obj = {
            type: options.type,
            data: data,
            options: {
                title: {
                    display: !0
                },
                tooltips: {
                    mode: "index",
                    intersect: !1
                },
                responsive: !0,
                scales: {
                    xAxes: [{
                        stacked: !0
                    }],
                    yAxes: [{
                        stacked: !0
                    }]
                }
            }
        };

        makeChart(options, obj);
    }

    store.call(options, onSuccess);
}

function makeLineChart(options) {

    function onSuccess(data) {
        obj = {
            type: options.type,
            data: data,
            options: {
                maintainAspectRatio: true,
                spanGaps: false,
                elements: {
                    line: {
                        tension: 0.000001
                    }
                },
                scales: {
                    yAxes: [{
                        stacked: true
                    }]
                },
                plugins: {
                    filler: {
                        propagate: false
                    },
                    'samples-filler-analyser': {
                        target: 'chart-analyser'
                    }
                }
            }
        };

        makeChart(options, obj);
    }

    store.call(options, onSuccess);
}

function makePolarArea(options) {
    function onSuccess(data) {
        obj = {
            type: options.type,
            data: data,
            options: {
                "elements": {
                    "line": {
                        "tension": 0,
                        "borderWidth": 3
                    }
                }
            }
        };

        makeChart(options, obj);
    }

    store.call(options, onSuccess);
}

store = function () {

    var optionsDic = [];

    ajax = function (options, onSuccess) {
        $.ajax({
            url: options.url,
            data: options.data,
            success: function (data) {
                optionsDic.push({ id: options.id, name: options.name, options: options, data: data, onSuccess: onSuccess });
                onSuccess(data);
            }
        })
    }

    run = function (runner) {

        callR = function () {

            setTimeout(function () {
                var r = runner.reverse().pop();
                if (r != null) {
                    r();
                    callR();
                }
            }, 20);
        };

        callR();
    }

    return {
        call: function (options, onSuccess) {
            var element = optionsDic.find(o => o.Id === options.id);
            if (!element)
            {   //first time
                ajax(options, onSuccess);
            }
            else
            {
                element.onSuccess(element.data);
            }
        },
        rebuildFromState: function (continent, callback) {
            
            var charts = optionsDic.filter(o => o.options.data.continent === continent);

            charts.forEach(o => window[o.name].destroy());            

            var runner = [];

            charts.forEach(o => runner.push(function () { o.onSuccess(o.data) }));

            if (callback)
                callback();

            run(runner);

        },
        state: function () {
            return optionsDic;
        },
        destroyCharts: function () {
            optionsDic.forEach(o => window[o.name].destroy());
        }
    }
}();