!function(a,b,c){"use strict";function f(){function a(a,c){return b.extend(Object.create(a),c)}function d(a,b){var c=b.caseInsensitiveMatch,d={originalPath:a,regexp:a},e=d.keys=[];return a=a.replace(/([().])/g,"\\$1").replace(/(\/)?:(\w+)([\?\*])?/g,function(a,b,c,d){var f="?"===d?d:null,g="*"===d?d:null;return e.push({name:c,optional:!!f}),b=b||"",""+(f?"":b)+"(?:"+(f?b:"")+(g&&"(.+?)"||"([^/]+)")+(f||"")+")"+(f||"")}).replace(/([\/$\*])/g,"\\$1"),d.regexp=new RegExp("^"+a+"$",c?"i":""),d}var c={};this.when=function(a,e){var f=b.copy(e);if(b.isUndefined(f.reloadOnSearch)&&(f.reloadOnSearch=!0),b.isUndefined(f.caseInsensitiveMatch)&&(f.caseInsensitiveMatch=this.caseInsensitiveMatch),c[a]=b.extend(f,a&&d(a,f)),a){var g="/"==a[a.length-1]?a.substr(0,a.length-1):a+"/";c[g]=b.extend({redirectTo:a},d(g,f))}return this},this.caseInsensitiveMatch=!1,this.otherwise=function(a){return"string"==typeof a&&(a={redirectTo:a}),this.when(null,a),this},this.$get=["$rootScope","$location","$routeParams","$q","$injector","$templateRequest","$sce",function(d,f,g,h,i,j,k){function p(a,b){var c=b.keys,d={};if(!b.regexp)return null;var e=b.regexp.exec(a);if(!e)return null;for(var f=1,g=e.length;f<g;++f){var h=c[f-1],i=e[f];h&&i&&(d[h.name]=i)}return d}function q(a){var c=o.current;m=s(),n=m&&c&&m.$$route===c.$$route&&b.equals(m.pathParams,c.pathParams)&&!m.reloadOnSearch&&!l,n||!c&&!m||d.$broadcast("$routeChangeStart",m,c).defaultPrevented&&a&&a.preventDefault()}function r(){var a=o.current,c=m;n?(a.params=c.params,b.copy(a.params,g),d.$broadcast("$routeUpdate",a)):(c||a)&&(l=!1,o.current=c,c&&c.redirectTo&&(b.isString(c.redirectTo)?f.path(t(c.redirectTo,c.params)).search(c.params).replace():f.url(c.redirectTo(c.pathParams,f.path(),f.search())).replace()),h.when(c).then(function(){if(c){var d,e,a=b.extend({},c.resolve);return b.forEach(a,function(c,d){a[d]=b.isString(c)?i.get(c):i.invoke(c,null,null,d)}),b.isDefined(d=c.template)?b.isFunction(d)&&(d=d(c.params)):b.isDefined(e=c.templateUrl)&&(b.isFunction(e)&&(e=e(c.params)),b.isDefined(e)&&(c.loadedTemplateUrl=k.valueOf(e),d=j(e))),b.isDefined(d)&&(a.$template=d),h.all(a)}}).then(function(e){c==o.current&&(c&&(c.locals=e,b.copy(c.params,g)),d.$broadcast("$routeChangeSuccess",c,a))},function(b){c==o.current&&d.$broadcast("$routeChangeError",c,a,b)}))}function s(){var d,e;return b.forEach(c,function(c,g){!e&&(d=p(f.path(),c))&&(e=a(c,{params:b.extend({},f.search(),d),pathParams:d}),e.$$route=c)}),e||c[null]&&a(c[null],{params:{},pathParams:{}})}function t(a,c){var d=[];return b.forEach((a||"").split(":"),function(a,b){if(0===b)d.push(a);else{var e=a.match(/(\w+)(?:[?*])?(.*)/),f=e[1];d.push(c[f]),d.push(e[2]||""),delete c[f]}}),d.join("")}var m,n,l=!1,o={routes:c,reload:function(){l=!0,d.$evalAsync(function(){q(),r()})},updateParams:function(a){if(!this.current||!this.current.$$route)throw e("norout","Tried updating route when with no current route");a=b.extend({},this.current.params,a),f.path(t(this.current.$$route.originalPath,a)),f.search(a)}};return d.$on("$locationChangeStart",q),d.$on("$locationChangeSuccess",r),o}]}function g(){this.$get=function(){return{}}}function h(a,c,d){return{restrict:"ECA",terminal:!0,priority:400,transclude:"element",link:function(e,f,g,h,i){function o(){l&&(d.cancel(l),l=null),j&&(j.$destroy(),j=null),k&&(l=d.leave(k),l.then(function(){l=null}),k=null)}function p(){var g=a.current&&a.current.locals,h=g&&g.$template;if(b.isDefined(h)){var l=e.$new(),p=a.current,q=i(l,function(a){d.enter(a,null,k||f).then(function(){!b.isDefined(m)||m&&!e.$eval(m)||c()}),o()});k=q,j=p.scope=l,j.$emit("$viewContentLoaded"),j.$eval(n)}else o()}var j,k,l,m=g.autoscroll,n=g.onload||"";e.$on("$routeChangeSuccess",p),p()}}}function i(a,b,c){return{restrict:"ECA",priority:-400,link:function(d,e){var f=c.current,g=f.locals;e.html(g.$template);var h=a(e.contents());if(f.controller){g.$scope=d;var i=b(f.controller,g);f.controllerAs&&(d[f.controllerAs]=i),e.data("$ngControllerController",i),e.children().data("$ngControllerController",i)}h(d)}}}var d=b.module("ngRoute",["ng"]).provider("$route",f),e=b.$$minErr("ngRoute");d.provider("$routeParams",g),d.directive("ngView",h),d.directive("ngView",i),h.$inject=["$route","$anchorScroll","$animate"],i.$inject=["$compile","$controller","$route"]}(window,window.angular);