var contrato = 0;
var tablaconsumos = {};
var tablapagos = {};
var deuda = {};
var cortes = {};

var url_api = "/api/ApiData/";

var url_home = "";

var app = angular.module("myApp", ['ngRoute', 'ngMap', 'chart.js']);
app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl: url_home + "/Home/Login",
        controller: "CtrlLogin"
    })
    .when("/Main", {
        templateUrl: url_home + "/Home/Main",
        controller: "CtrlAuth"
    })
    .when("/ConsultaDeuda", {
        templateUrl: url_home + "/Home/ConsultaDeuda",
        controller: "CtrlConsultaDeuda"
    })
    .when("/UltimosCosumos", {
        templateUrl: url_home + "/Home/UltimosCosumos",
        controller: "CtrlUltimosCosumos"
    })
    .when("/UltimosPagos", {
        templateUrl: url_home + "/Home/UltimosPagos",
        controller: "CtrlUltimosPagos"
    })
    .when("/LugaresPago", {
        templateUrl: url_home + "/Home/LugaresPago",
        controller: "CtrlLugaresPago"
    })
    .when("/CortesProgramados", {
        templateUrl: url_home + "/Home/CortesProgramados",
        controller: "CtrlCortesProgramados"
    })
    .when("/CortesNoProgramados", {
        templateUrl: url_home + "/Home/CortesNoProgramados",
        controller: "CtrlAuth"
    })
    .when("/ConsultaReclamo", {
        templateUrl: url_home + "/Home/ConsultaReclamo",
        controller: "CtrlAuth"
    })
    .when("/ReqCambioNombre", {
        templateUrl: url_home + "/Home/ReqCambioNombre",
        controller: "CtrlAuth"
    })
    .when("/ReqNuevoSuministro", {
        templateUrl: url_home + "/Home/ReqNuevoSuministro",
        controller: "CtrlAuth"
    })
    .when("/ReqAumentoCarga", {
        templateUrl: url_home + "/Home/ReqAumentoCarga",
        controller: "CtrlAuth"
    })
    .when("/ReqCorteDefinitivo", {
        templateUrl: url_home + "/Home/ReqCorteDefinitivo",
        controller: "CtrlAuth"
    })
    .when("/ReqReubicacionMedidor", {
        templateUrl: url_home + "/Home/ReqReubicacionMedidor",
        controller: "CtrlAuth"
    })
    .when("/ReqReubicacionMedidor", {
        templateUrl: url_home + "/Home/ReqReubicacionMedidor",
        controller: "CtrlAuth"
    })
});

app.controller('CtrlAuth', function ($scope, $location) {
    if (contrato == 0) {
        $location.path("/");
    }
});

app.controller('CtrlLogin', function ($scope, $location, $http) {

    $scope.diverror = false;
    $scope.contrato = "";
    $scope.password = ""

    $scope.login = function () {
        console.log($scope.contrato);
        if ($scope.contrato == "") {
            $scope.error = "Ingrese Contrato";
            $scope.diverror = true;
        }
        else {
            $scope.diverror = false;
            $http.get(url_api + $scope.contrato)
            .then(function (response) {
                if (parseInt(response.data.ERROR) == 1) {
                    $scope.error = response.data.ERROR_MSG;
                    $scope.diverror = true;
                }
                else {
                    contrato = $scope.contrato;
                    tablaconsumos = response.data.Consumos;
                    tablapagos = response.data.Pagos;
                    deuda = response.data.Deuda;
                    cortes = response.data.Cortes;
                    $location.path("/Main");
                }
            });
        }
    };
});

app.controller('CtrlUltimosCosumos', function ($scope, $location, $http) {
    if (contrato == 0) {
        $location.path("/");
    }
    $scope.tabledata = tablaconsumos;

    var labels = [];
    var data = [];

    for (i = 0; i < tablaconsumos.length; i++) {
        labels.push(tablaconsumos[i].Periodo);
        data.push(tablaconsumos[i].C_EA);
    }  

    $scope.labels = labels;
    $scope.series = ['Cosumos'];
    $scope.contrato = contrato;

    $scope.data = [
      data
    ];

});

app.controller('CtrlUltimosPagos', function ($scope, $location, $http) {
    if (contrato == 0) {
        $location.path("/");
    }
    $scope.tabledata = tablapagos;

    var labels = [];
    var data = [];

    for (i = 0; i < tablapagos.length; i++) {
        labels.push(tablapagos[i].CodigoPeriodoComercial);
        data.push(tablapagos[i].MontoCobrado);
    }

    $scope.labels = labels;
    $scope.series = ['Pagos'];
    $scope.contrato = contrato;

    $scope.data = [
      data
    ];
});

app.controller('CtrlConsultaDeuda', function ($scope, $location, $http) {
    if (contrato == 0) {
        $location.path("/");
    }

    $scope.MontoDeudaTotal = deuda.MontoDeudaTotal;
    $scope.UltimoDiaPago = deuda.UltimoDiaPago;
    $scope.contrato = contrato;
 
});

app.controller('CtrlCortesProgramados', function ($scope, $location, $http) {
    if (contrato == 0) {
        $location.path("/");
    }
    $scope.tablecortes = cortes;   
});

app.controller('CtrlLugaresPago', function ($scope, $location, $http) {
    if (contrato == 0) {
        $location.path("/");
    }
    //var vm = this;
    $scope.latlng = __lat + ", " + __lon;//-16.398795, -71.536893    
});

app.directive('tabs', function () {
    return {
        restrict: 'E',
        transclude: true,
        scope: {},
        controller: ["$scope", function ($scope) {
            var panes = $scope.panes = [];

            $scope.select = function (pane) {
                angular.forEach(panes, function (pane) {
                    pane.selected = false;
                });
                pane.selected = true;
            }

            this.addPane = function (pane) {
                if (panes.length == 0) $scope.select(pane);
                panes.push(pane);
            }
        }],
        template:
          '<div class="tabbable">' +
            '<ul class="nav nav-tabs">' +
              '<li ng-repeat="pane in panes" ng-class="{active:pane.selected}">' +
                '<a href="" ng-click="select(pane)"><i class="{{pane.title}}"></a>' +
              '</li>' +
            '</ul>' +
            '<div class="tab-content" ng-transclude></div>' +
          '</div>',
        replace: true
    };
});

app.directive('pane', function () {
    return {
        require: '^tabs',
        restrict: 'E',
        transclude: true,
        scope: { title: '@' },
        link: function (scope, element, attrs, tabsCtrl) {
            tabsCtrl.addPane(scope);
        },
        template:
          '<div class="tab-pane" ng-class="{active: selected}" ng-transclude>' +
          '</div>',
        replace: true
    };
});