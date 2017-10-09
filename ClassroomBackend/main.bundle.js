webpackJsonp([1],{

/***/ "../../../../../src async recursive":
/***/ (function(module, exports) {

function webpackEmptyContext(req) {
	throw new Error("Cannot find module '" + req + "'.");
}
webpackEmptyContext.keys = function() { return []; };
webpackEmptyContext.resolve = webpackEmptyContext;
module.exports = webpackEmptyContext;
webpackEmptyContext.id = "../../../../../src async recursive";

/***/ }),

/***/ "../../../../../src/app/app.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/app.component.html":
/***/ (function(module, exports) {

module.exports = "<!--The content below is only a placeholder and can be replaced.-->\r\n<div style=\"text-align:center\">\r\n<app-studentlist [slist]=\"slist\"></app-studentlist>\r\n"

/***/ }),

/***/ "../../../../../src/app/app.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var AppComponent = (function () {
    function AppComponent(elm) {
        this.title = 'app';
        // this.slist = JSON.parse("{\"n\":\"Joe\"}");
        // this.slist = JSON.parse("[{stid: 1, date: '20170108', name: 'Joe'}]");
        this.slist = JSON.parse(elm.nativeElement.getAttribute('slist').slice());
        // console.log(this.slist);
    }
    return AppComponent;
}());
AppComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_0" /* Component */])({
        selector: 'app-root',
        template: __webpack_require__("../../../../../src/app/app.component.html"),
        styles: [__webpack_require__("../../../../../src/app/app.component.css")]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["M" /* ElementRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["M" /* ElementRef */]) === "function" && _a || Object])
], AppComponent);

var _a;
//# sourceMappingURL=app.component.js.map

/***/ }),

/***/ "../../../../../src/app/app.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__ = __webpack_require__("../../../platform-browser/@angular/platform-browser.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__app_component__ = __webpack_require__("../../../../../src/app/app.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__studentlist_studentlist_component__ = __webpack_require__("../../../../../src/app/studentlist/studentlist.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_core__["b" /* NgModule */])({
        declarations: [
            __WEBPACK_IMPORTED_MODULE_2__app_component__["a" /* AppComponent */],
            __WEBPACK_IMPORTED_MODULE_3__studentlist_studentlist_component__["a" /* StudentlistComponent */]
        ],
        imports: [
            __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__["a" /* BrowserModule */]
        ],
        providers: [],
        bootstrap: [__WEBPACK_IMPORTED_MODULE_2__app_component__["a" /* AppComponent */]]
    })
], AppModule);

//# sourceMappingURL=app.module.js.map

/***/ }),

/***/ "../../../../../src/app/studentlist/studentlist.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "h2 {\r\n  text-align: center;\r\n  border-bottom: 1px solid darkred;\r\n}\r\ndiv {\r\n  text-align: left;\r\n  padding:3px;\r\n}\r\n\r\ndiv.datemark {\r\n  color: white;\r\n  font-weight: bolder;\r\n  background-color: #000066;\r\n}\r\n\r\ndiv.student {\r\n  padding: .5em 0 .5em 2em;\r\n  background-color: ghostwhite;\r\n  min-width: 20em;\r\n\tdisplay: inline-block;\r\n  margin: 2px;\r\n  cursor: pointer;\r\n}\r\ndiv.student.sent {\r\n  background-color: lawngreen;\r\n}\r\ndiv.student.present {\r\n  background-color: yellow;\r\n}\r\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/studentlist/studentlist.component.html":
/***/ (function(module, exports) {

module.exports = "  <h2>Student Checkin</h2>\r\n  <div>\r\n      <ng-template ngFor let-student [ngForOf]=\"slist\">\r\n          <div *ngIf=\"checkDate(student.d)\" class=\"datemark\">\r\n              {{getdate(student.d) | date:'MMM d, y'}}\r\n          </div>\r\n        <div [ngClass]=\"student.c + ' student'\"\r\n          (click)=\"gotClicked(student.s)\">{{student.n}}\r\n        </div>\r\n        </ng-template>\r\n  </div>\r\n\r\n"

/***/ }),

/***/ "../../../../../src/app/studentlist/studentlist.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return StudentlistComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var StudentlistComponent = (function () {
    function StudentlistComponent(changeDetector) {
        this.changeDetector = changeDetector;
    }
    StudentlistComponent.prototype.ngOnInit = function () {
        this.sortslist();
        var my = this;
        $.connection.hub.url = 'http://localhost:55199/signalr'; // TESTING ONLY
        // Declare a proxy to reference the hub.
        this.ClassHub = $.connection.classHub;
        // Create a function that the hub can call to broadcast messages.
        this.ClassHub.client.broadcastCheckin = function (stid, status) {
            if (status) {
                my.setStatus(stid, 'present');
            }
            else {
                my.setStatus(stid, '');
            }
            my.changeDetector.detectChanges();
        };
        $.connection.hub.start()
            .done(function () {
            my.ClassHub.server.joinGroup(1);
        });
        // set up initial display
        console.log(this.slist);
        this.slist.forEach(function (element) {
            if (element.p) {
                element.c = 'present';
            }
        });
    };
    StudentlistComponent.prototype.sendMessage = function (stid, status) {
        this.ClassHub.server.checkin(1, stid, status);
    };
    StudentlistComponent.prototype.checkDate = function (newdate) {
        if (newdate === this.lastdate) {
            return false;
        }
        this.lastdate = newdate;
        return true;
    };
    StudentlistComponent.prototype.getdate = function (ds) {
        var d = new Date(+ds.substr(0, 4), +ds.substr(4, 2), +ds.substr(6));
        return d;
    };
    StudentlistComponent.prototype.gotClicked = function (id) {
        switch (this.getStatus(id)) {
            case 'present':
                this.setStatus(id, ''); // preemptively
                this.sendMessage(id, false);
                break;
            default:
                this.setStatus(id, 'sent');
                this.sendMessage(id, true);
        }
    };
    StudentlistComponent.prototype.getStatus = function (id) {
        var r = '';
        this.slist.forEach(function (element) {
            if (id === element.s) {
                r = element.c;
            }
        });
        return r;
    };
    StudentlistComponent.prototype.setStatus = function (id, newstat) {
        this.slist.forEach(function (element, i) {
            if (id === element.s) {
                element.c = newstat;
                // console.log(this.slist);
            }
        });
    };
    StudentlistComponent.prototype.sortslist = function () {
        this.slist.sort(function (a, b) {
            if (a.d < b.d) {
                return -1;
            }
            else if (a.d > b.d) {
                return 1;
            }
            else if (a.n < b.n) {
                return -1;
            }
            else if (a.n > b.n) {
                return 1;
            }
            else {
                return 0;
            }
        });
        // console.log(this.slist);
    };
    return StudentlistComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["O" /* Input */])(),
    __metadata("design:type", Array)
], StudentlistComponent.prototype, "slist", void 0);
StudentlistComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_0" /* Component */])({
        selector: 'app-studentlist',
        template: __webpack_require__("../../../../../src/app/studentlist/studentlist.component.html"),
        styles: [__webpack_require__("../../../../../src/app/studentlist/studentlist.component.css")]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["Z" /* ChangeDetectorRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["Z" /* ChangeDetectorRef */]) === "function" && _a || Object])
], StudentlistComponent);

var _a;
//# sourceMappingURL=studentlist.component.js.map

/***/ }),

/***/ "../../../../../src/environments/environment.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return environment; });
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
// The file contents for the current environment will overwrite these during build.
var environment = {
    production: false
};
//# sourceMappingURL=environment.js.map

/***/ }),

/***/ "../../../../../src/main.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__ = __webpack_require__("../../../platform-browser-dynamic/@angular/platform-browser-dynamic.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__app_app_module__ = __webpack_require__("../../../../../src/app/app.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");




if (__WEBPACK_IMPORTED_MODULE_3__environments_environment__["a" /* environment */].production) {
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["a" /* enableProdMode */])();
}
__webpack_require__.i(__WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__["a" /* platformBrowserDynamic */])().bootstrapModule(__WEBPACK_IMPORTED_MODULE_2__app_app_module__["a" /* AppModule */]);
//# sourceMappingURL=main.js.map

/***/ }),

/***/ 0:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__("../../../../../src/main.ts");


/***/ })

},[0]);
//# sourceMappingURL=main.bundle.js.map