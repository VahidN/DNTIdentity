(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"],{

/***/ "./src/$$_lazy_route_resource lazy recursive":
/*!**********************************************************!*\
  !*** ./src/$$_lazy_route_resource lazy namespace object ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncaught exception popping up in devtools
	return Promise.resolve().then(function() {
		var e = new Error('Cannot find module "' + req + '".');
		e.code = 'MODULE_NOT_FOUND';
		throw e;
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "./src/$$_lazy_route_resource lazy recursive";

/***/ }),

/***/ "./src/app/app-routing.module.ts":
/*!***************************************!*\
  !*** ./src/app/app-routing.module.ts ***!
  \***************************************/
/*! exports provided: AppRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppRoutingModule", function() { return AppRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _page_not_found_page_not_found_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./page-not-found/page-not-found.component */ "./src/app/page-not-found/page-not-found.component.ts");
/* harmony import */ var _welcome_welcome_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./welcome/welcome.component */ "./src/app/welcome/welcome.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




var routes = [
    { path: "welcome", component: _welcome_welcome_component__WEBPACK_IMPORTED_MODULE_3__["WelcomeComponent"] },
    { path: "", redirectTo: "welcome", pathMatch: "full" },
    { path: "**", component: _page_not_found_page_not_found_component__WEBPACK_IMPORTED_MODULE_2__["PageNotFoundComponent"] }
];
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forRoot(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());



/***/ }),

/***/ "./src/app/app.component.css":
/*!***********************************!*\
  !*** ./src/app/app.component.css ***!
  \***********************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/app.component.html":
/*!************************************!*\
  !*** ./src/app/app.component.html ***!
  \************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<app-header></app-header>\n<div class=\"container p-5\">\n  <router-outlet></router-outlet>\n</div>\n"

/***/ }),

/***/ "./src/app/app.component.ts":
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/*! exports provided: AppComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppComponent", function() { return AppComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var app_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! app/core */ "./src/app/core/index.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var AppComponent = /** @class */ (function () {
    function AppComponent(refreshTokenService) {
        this.refreshTokenService = refreshTokenService;
    }
    AppComponent.prototype.unloadHandler = function () {
        // Invalidate current tab as active RefreshToken timer
        this.refreshTokenService.invalidateCurrentTabId();
    };
    AppComponent.prototype.beforeUnloadHander = function () {
        // ...
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])("window:unload", ["$event"]),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", []),
        __metadata("design:returntype", void 0)
    ], AppComponent.prototype, "unloadHandler", null);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["HostListener"])("window:beforeunload", ["$event"]),
        __metadata("design:type", Function),
        __metadata("design:paramtypes", []),
        __metadata("design:returntype", void 0)
    ], AppComponent.prototype, "beforeUnloadHander", null);
    AppComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: "app-root",
            template: __webpack_require__(/*! ./app.component.html */ "./src/app/app.component.html"),
            styles: [__webpack_require__(/*! ./app.component.css */ "./src/app/app.component.css")]
        }),
        __metadata("design:paramtypes", [app_core__WEBPACK_IMPORTED_MODULE_1__["RefreshTokenService"]])
    ], AppComponent);
    return AppComponent;
}());



/***/ }),

/***/ "./src/app/app.module.ts":
/*!*******************************!*\
  !*** ./src/app/app.module.ts ***!
  \*******************************/
/*! exports provided: AppModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppModule", function() { return AppModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser */ "./node_modules/@angular/platform-browser/fesm5/platform-browser.js");
/* harmony import */ var _app_routing_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./app-routing.module */ "./src/app/app-routing.module.ts");
/* harmony import */ var _app_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./app.component */ "./src/app/app.component.ts");
/* harmony import */ var _authentication_authentication_module__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./authentication/authentication.module */ "./src/app/authentication/authentication.module.ts");
/* harmony import */ var _core_core_module__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./core/core.module */ "./src/app/core/core.module.ts");
/* harmony import */ var _dashboard_dashboard_module__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./dashboard/dashboard.module */ "./src/app/dashboard/dashboard.module.ts");
/* harmony import */ var _page_not_found_page_not_found_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./page-not-found/page-not-found.component */ "./src/app/page-not-found/page-not-found.component.ts");
/* harmony import */ var _shared_shared_module__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./shared/shared.module */ "./src/app/shared/shared.module.ts");
/* harmony import */ var _welcome_welcome_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./welcome/welcome.component */ "./src/app/welcome/welcome.component.ts");
/* harmony import */ var _request_request_module__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./request/request.module */ "./src/app/request/request.module.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};











var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [
                _app_component__WEBPACK_IMPORTED_MODULE_3__["AppComponent"],
                _welcome_welcome_component__WEBPACK_IMPORTED_MODULE_9__["WelcomeComponent"],
                _page_not_found_page_not_found_component__WEBPACK_IMPORTED_MODULE_7__["PageNotFoundComponent"]
            ],
            imports: [
                _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__["BrowserModule"],
                _core_core_module__WEBPACK_IMPORTED_MODULE_5__["CoreModule"],
                _shared_shared_module__WEBPACK_IMPORTED_MODULE_8__["SharedModule"].forRoot(),
                _authentication_authentication_module__WEBPACK_IMPORTED_MODULE_4__["AuthenticationModule"],
                _dashboard_dashboard_module__WEBPACK_IMPORTED_MODULE_6__["DashboardModule"],
                _request_request_module__WEBPACK_IMPORTED_MODULE_10__["RequestModule"],
                _app_routing_module__WEBPACK_IMPORTED_MODULE_2__["AppRoutingModule"]
            ],
            providers: [],
            bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_3__["AppComponent"]]
        })
    ], AppModule);
    return AppModule;
}());



/***/ }),

/***/ "./src/app/authentication/access-denied/access-denied.component.css":
/*!**************************************************************************!*\
  !*** ./src/app/authentication/access-denied/access-denied.component.css ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/authentication/access-denied/access-denied.component.html":
/*!***************************************************************************!*\
  !*** ./src/app/authentication/access-denied/access-denied.component.html ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h1 class=\"text-danger\">\n  <i class=\"fa fa-ban\" aria-hidden=\"true\"></i> Access Denied\n</h1>\n<p>Sorry! You don't have access to this page.</p>\n<button class=\"btn btn-default\" (click)=\"goBack()\">\n  <i class=\"fa fa-arrow-left\" aria-hidden=\"true\"></i> Back\n</button>\n\n<button *ngIf=\"!isAuthenticated\" class=\"btn btn-success\" [routerLink]=\"['/login']\"\n  queryParamsHandling=\"merge\">\n  Login\n</button>\n"

/***/ }),

/***/ "./src/app/authentication/access-denied/access-denied.component.ts":
/*!*************************************************************************!*\
  !*** ./src/app/authentication/access-denied/access-denied.component.ts ***!
  \*************************************************************************/
/*! exports provided: AccessDeniedComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AccessDeniedComponent", function() { return AccessDeniedComponent; });
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _app_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @app/core */ "./src/app/core/index.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var AccessDeniedComponent = /** @class */ (function () {
    function AccessDeniedComponent(location, authService) {
        this.location = location;
        this.authService = authService;
        this.isAuthenticated = false;
    }
    AccessDeniedComponent.prototype.ngOnInit = function () {
        this.isAuthenticated = this.authService.isAuthUserLoggedIn();
    };
    AccessDeniedComponent.prototype.goBack = function () {
        this.location.back(); // <-- go back to previous location on cancel
    };
    AccessDeniedComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: "app-access-denied",
            template: __webpack_require__(/*! ./access-denied.component.html */ "./src/app/authentication/access-denied/access-denied.component.html"),
            styles: [__webpack_require__(/*! ./access-denied.component.css */ "./src/app/authentication/access-denied/access-denied.component.css")]
        }),
        __metadata("design:paramtypes", [_angular_common__WEBPACK_IMPORTED_MODULE_0__["Location"],
            _app_core__WEBPACK_IMPORTED_MODULE_2__["AuthService"]])
    ], AccessDeniedComponent);
    return AccessDeniedComponent;
}());



/***/ }),

/***/ "./src/app/authentication/authentication-routing.module.ts":
/*!*****************************************************************!*\
  !*** ./src/app/authentication/authentication-routing.module.ts ***!
  \*****************************************************************/
/*! exports provided: AuthenticationRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthenticationRoutingModule", function() { return AuthenticationRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _app_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @app/core */ "./src/app/core/index.ts");
/* harmony import */ var _access_denied_access_denied_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./access-denied/access-denied.component */ "./src/app/authentication/access-denied/access-denied.component.ts");
/* harmony import */ var _change_password_change_password_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./change-password/change-password.component */ "./src/app/authentication/change-password/change-password.component.ts");
/* harmony import */ var _login_login_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./login/login.component */ "./src/app/authentication/login/login.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






var routes = [
    { path: "login", component: _login_login_component__WEBPACK_IMPORTED_MODULE_5__["LoginComponent"] },
    { path: "accessDenied", component: _access_denied_access_denied_component__WEBPACK_IMPORTED_MODULE_3__["AccessDeniedComponent"] },
    {
        path: "changePassword", component: _change_password_change_password_component__WEBPACK_IMPORTED_MODULE_4__["ChangePasswordComponent"],
        data: {
            permission: {
                permittedRoles: ["Admin", "test", "User"]
            }
        },
        canActivate: [_app_core__WEBPACK_IMPORTED_MODULE_2__["AuthGuard"]]
    }
];
var AuthenticationRoutingModule = /** @class */ (function () {
    function AuthenticationRoutingModule() {
    }
    AuthenticationRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], AuthenticationRoutingModule);
    return AuthenticationRoutingModule;
}());



/***/ }),

/***/ "./src/app/authentication/authentication.module.ts":
/*!*********************************************************!*\
  !*** ./src/app/authentication/authentication.module.ts ***!
  \*********************************************************/
/*! exports provided: AuthenticationModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthenticationModule", function() { return AuthenticationModule; });
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _app_shared_shared_module__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @app/shared/shared.module */ "./src/app/shared/shared.module.ts");
/* harmony import */ var _access_denied_access_denied_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./access-denied/access-denied.component */ "./src/app/authentication/access-denied/access-denied.component.ts");
/* harmony import */ var _authentication_routing_module__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./authentication-routing.module */ "./src/app/authentication/authentication-routing.module.ts");
/* harmony import */ var _change_password_change_password_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./change-password/change-password.component */ "./src/app/authentication/change-password/change-password.component.ts");
/* harmony import */ var _change_password_services_change_password_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./change-password/services/change-password.service */ "./src/app/authentication/change-password/services/change-password.service.ts");
/* harmony import */ var _login_login_component__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./login/login.component */ "./src/app/authentication/login/login.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};









var AuthenticationModule = /** @class */ (function () {
    function AuthenticationModule() {
    }
    AuthenticationModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"],
                _app_shared_shared_module__WEBPACK_IMPORTED_MODULE_3__["SharedModule"],
                _authentication_routing_module__WEBPACK_IMPORTED_MODULE_5__["AuthenticationRoutingModule"]
            ],
            declarations: [_login_login_component__WEBPACK_IMPORTED_MODULE_8__["LoginComponent"], _access_denied_access_denied_component__WEBPACK_IMPORTED_MODULE_4__["AccessDeniedComponent"], _change_password_change_password_component__WEBPACK_IMPORTED_MODULE_6__["ChangePasswordComponent"]],
            providers: [_change_password_services_change_password_service__WEBPACK_IMPORTED_MODULE_7__["ChangePasswordService"]]
        })
    ], AuthenticationModule);
    return AuthenticationModule;
}());



/***/ }),

/***/ "./src/app/authentication/change-password/change-password.component.css":
/*!******************************************************************************!*\
  !*** ./src/app/authentication/change-password/change-password.component.css ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/authentication/change-password/change-password.component.html":
/*!*******************************************************************************!*\
  !*** ./src/app/authentication/change-password/change-password.component.html ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"card\">\n  <div class=\"card-header\">\n    <h2 class=\"card-title\">Change Password</h2>\n  </div>\n  <div class=\"card-body\">\n    <div class=\"container\">\n      <form #form=\"ngForm\" (submit)=\"submitForm(form)\" novalidate>\n\n        <div class=\"form-group\">\n          <label class=\"control-label\">Current Password</label>\n          <input name=\"oldPassword\" #oldPassword=\"ngModel\" [class.is-invalid]=\"oldPassword.invalid && oldPassword.touched\"\n            type=\"password\" required class=\"form-control\" name=\"password\" [(ngModel)]=\"model.oldPassword\">\n          <ng-container *ngTemplateOutlet=\"validationErrorsTemplate; context:{ control: oldPassword }\"></ng-container>\n        </div>\n\n        <div class=\"form-group\">\n          <label class=\"control-label\">New Password</label>\n          <input name=\"newPassword\" #newPassword=\"ngModel\" [class.is-invalid]=\"newPassword.invalid && newPassword.touched\"\n            type=\"password\" required minlength=\"4\" class=\"form-control\" name=\"newPassword\"\n            appValidateEqual compare-to=\"confirmPassword\" [(ngModel)]=\"model.newPassword\">\n          <ng-container *ngTemplateOutlet=\"validationErrorsTemplate; context:{ control: newPassword }\"></ng-container>\n        </div>\n\n        <div class=\"form-group\">\n          <label class=\"control-label\">Confirm Password</label>\n          <input name=\"confirmPassword\" #confirmPassword=\"ngModel\" [class.is-invalid]=\"confirmPassword.invalid && confirmPassword.touched\"\n            type=\"password\" required minlength=\"4\" class=\"form-control\" name=\"confirmPassword\"\n            appValidateEqual compare-to=\"newPassword\" [(ngModel)]=\"model.confirmPassword\">\n          <ng-container *ngTemplateOutlet=\"validationErrorsTemplate; context:{ control: confirmPassword }\"></ng-container>\n        </div>\n\n        <button class=\"btn btn-primary\" [disabled]=\"form.invalid\" type=\"submit\">Submit</button>\n\n        <div *ngIf=\"error\" class=\"alert alert-danger \" role=\"alert \">\n          {{error}}\n        </div>\n      </form>\n    </div>\n  </div>\n</div>\n\n<ng-template #validationErrorsTemplate let-ctrl=\"control\">\n  <div *ngIf=\"ctrl.invalid && ctrl.touched\">\n    <div class=\"alert alert-danger\" *ngIf=\"ctrl.errors.required\">\n      This field is required.\n    </div>\n    <div class=\"alert alert-danger\" *ngIf=\"ctrl.errors.minlength\">\n      This field should be minimum {{ctrl.errors.minlength.requiredLength}} characters.\n    </div>\n    <div class=\"alert alert-danger\" *ngIf=\"ctrl.errors.maxlength\">\n      This field should be max {{ctrl.errors.maxlength.requiredLength}} characters.\n    </div>\n    <div class=\"alert alert-danger\" *ngIf=\"ctrl.errors.pattern\">\n      This field's pattern: {{ctrl.errors.pattern.requiredPattern}}\n    </div>\n    <div class=\"alert alert-danger\" *ngIf=\"ctrl.errors.modelStateError\">\n      {{ctrl.errors.modelStateError.error}}\n    </div>\n    <div class=\"alert alert-danger\" *ngIf=\"ctrl.errors.appValidateEqual\">\n      Password mismatch.\n    </div>\n  </div>\n</ng-template>\n"

/***/ }),

/***/ "./src/app/authentication/change-password/change-password.component.ts":
/*!*****************************************************************************!*\
  !*** ./src/app/authentication/change-password/change-password.component.ts ***!
  \*****************************************************************************/
/*! exports provided: ChangePasswordComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ChangePasswordComponent", function() { return ChangePasswordComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_change_password_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./services/change-password.service */ "./src/app/authentication/change-password/services/change-password.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var ChangePasswordComponent = /** @class */ (function () {
    function ChangePasswordComponent(router, changePasswordService) {
        this.router = router;
        this.changePasswordService = changePasswordService;
        this.error = "";
        this.model = {
            oldPassword: "",
            newPassword: "",
            confirmPassword: ""
        };
    }
    ChangePasswordComponent.prototype.ngOnInit = function () {
    };
    ChangePasswordComponent.prototype.submitForm = function (form) {
        var _this = this;
        console.log(this.model);
        console.log(form.value);
        this.error = "";
        this.changePasswordService.changePassword(this.model)
            .subscribe(function () {
            _this.router.navigate(["/welcome"]);
        }, function (error) {
            console.error("ChangePassword error", error);
            _this.error = error.error + " -> " + error.statusText + ": " + error.message;
        });
    };
    ChangePasswordComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: "app-change-password",
            template: __webpack_require__(/*! ./change-password.component.html */ "./src/app/authentication/change-password/change-password.component.html"),
            styles: [__webpack_require__(/*! ./change-password.component.css */ "./src/app/authentication/change-password/change-password.component.css")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"], _services_change_password_service__WEBPACK_IMPORTED_MODULE_2__["ChangePasswordService"]])
    ], ChangePasswordComponent);
    return ChangePasswordComponent;
}());



/***/ }),

/***/ "./src/app/authentication/change-password/services/change-password.service.ts":
/*!************************************************************************************!*\
  !*** ./src/app/authentication/change-password/services/change-password.service.ts ***!
  \************************************************************************************/
/*! exports provided: ChangePasswordService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ChangePasswordService", function() { return ChangePasswordService; });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _app_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @app/core */ "./src/app/core/index.ts");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var ChangePasswordService = /** @class */ (function () {
    function ChangePasswordService(http, appConfig) {
        this.http = http;
        this.appConfig = appConfig;
    }
    ChangePasswordService.prototype.changePassword = function (model) {
        var headers = new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpHeaders"]({ "Content-Type": "application/json" });
        var url = this.appConfig.apiEndpoint + "/ChangePassword";
        return this.http
            .post(url, model, { headers: headers })
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (response) { return response || {}; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["catchError"])(function (error) { return Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["throwError"])(error); }));
    };
    ChangePasswordService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])(),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Inject"])(_app_core__WEBPACK_IMPORTED_MODULE_2__["APP_CONFIG"])),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpClient"], Object])
    ], ChangePasswordService);
    return ChangePasswordService;
}());



/***/ }),

/***/ "./src/app/authentication/login/login.component.css":
/*!**********************************************************!*\
  !*** ./src/app/authentication/login/login.component.css ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/authentication/login/login.component.html":
/*!***********************************************************!*\
  !*** ./src/app/authentication/login/login.component.html ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"card\">\n  <div class=\"card-header\">\n    <h2 class=\"card-title\">Login</h2>\n  </div>\n  <div class=\"card-body\">\n    <form #form=\"ngForm\" (submit)=\"submitForm(form)\" novalidate>\n      <div class=\"form-group\">\n        <label for=\"username\">User name</label>\n        <input id=\"username\" type=\"text\" required name=\"username\" [(ngModel)]=\"model.username\"\n          [class.is-invalid]=\"username.invalid && username.touched\" #username=\"ngModel\"\n          class=\"form-control\" placeholder=\"User name\">\n        <div *ngIf=\"username.invalid && username.touched\">\n          <div class=\"alert alert-danger\"  *ngIf=\"username.errors['required']\">\n            Name is required.\n          </div>\n        </div>\n      </div>\n\n      <div class=\"form-group\">\n        <label for=\"password\">Password</label>\n        <input id=\"password\" type=\"password\" required name=\"password\" [(ngModel)]=\"model.password\"\n          [class.is-invalid]=\"password.invalid && password.touched\" #password=\"ngModel\"\n          class=\"form-control\" placeholder=\"Password\">\n        <div *ngIf=\"password.invalid && password.touched\">\n          <div class=\"alert alert-danger\"  *ngIf=\"password.errors['required']\">\n            Password is required.\n          </div>\n        </div>\n      </div>\n\n      <div class=\"form-check\">\n        <label>\n          <input type=\"checkbox\" name=\"rememberMe\" [(ngModel)]=\"model.rememberMe\"> Remember me\n        </label>\n      </div>\n\n      <div class=\"form-group\">\n        <button type=\"submit\" class=\"btn btn-primary\" [disabled]=\"form.invalid \">Login</button>\n      </div>\n\n      <div *ngIf=\"error\" class=\"alert alert-danger \" role=\"alert \">\n        {{error}}\n      </div>\n    </form>\n  </div>\n</div>\n"

/***/ }),

/***/ "./src/app/authentication/login/login.component.ts":
/*!*********************************************************!*\
  !*** ./src/app/authentication/login/login.component.ts ***!
  \*********************************************************/
/*! exports provided: LoginComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LoginComponent", function() { return LoginComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _app_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @app/core */ "./src/app/core/index.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var LoginComponent = /** @class */ (function () {
    function LoginComponent(authService, router, route) {
        this.authService = authService;
        this.router = router;
        this.route = route;
        this.model = { username: "", password: "", rememberMe: false };
        this.error = "";
        this.returnUrl = null;
    }
    LoginComponent.prototype.ngOnInit = function () {
        // reset the login status
        this.authService.logout(false);
        // get the return url from route parameters
        this.returnUrl = this.route.snapshot.queryParams["returnUrl"];
    };
    LoginComponent.prototype.submitForm = function (form) {
        var _this = this;
        console.log(form);
        this.error = "";
        this.authService.login(this.model)
            .subscribe(function (isLoggedIn) {
            if (isLoggedIn) {
                if (_this.returnUrl) {
                    _this.router.navigate([_this.returnUrl]);
                }
                else {
                    _this.router.navigate(["/protectedPage"]);
                }
            }
        }, function (error) {
            console.error("Login error", error);
            if (error.status === 401) {
                _this.error = "Invalid User name or Password. Please try again.";
            }
            else {
                _this.error = error.statusText + ": " + error.message;
            }
        });
    };
    LoginComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: "app-login",
            template: __webpack_require__(/*! ./login.component.html */ "./src/app/authentication/login/login.component.html"),
            styles: [__webpack_require__(/*! ./login.component.css */ "./src/app/authentication/login/login.component.css")]
        }),
        __metadata("design:paramtypes", [_app_core__WEBPACK_IMPORTED_MODULE_2__["AuthService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"]])
    ], LoginComponent);
    return LoginComponent;
}());



/***/ }),

/***/ "./src/app/core/component/header/header.component.css":
/*!************************************************************!*\
  !*** ./src/app/core/component/header/header.component.css ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/core/component/header/header.component.html":
/*!*************************************************************!*\
  !*** ./src/app/core/component/header/header.component.html ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<nav class=\"navbar navbar-light bg-light navbar-expand-md\">\n  <a class=\"navbar-brand\" [routerLink]=\"['/']\">{{title}}</a>\n  <ul class=\"nav navbar-nav\">\n    <li class=\"nav-item\" routerLinkActive=\"active\" [routerLinkActiveOptions]=\"{ exact: true }\">\n      <a class=\"nav-link\" [routerLink]=\"['/welcome']\">Home</a>\n    </li>\n    <li *ngIf=\"!isLoggedIn\" class=\"nav-item\" routerLinkActive=\"active\">\n      <a class=\"nav-link\" queryParamsHandling=\"merge\" [routerLink]=\"['/login']\">Login</a>\n    </li>\n    <li *ngIf=\"isLoggedIn\" class=\"nav-item\" routerLinkActive=\"active\">\n      <a class=\"nav-link\" (click)=\"logout()\">Logoff [{{displayName}}]</a>\n    </li>\n    <li *ngIf=\"isLoggedIn\" class=\"nav-item\" routerLinkActive=\"active\">\n      <a class=\"nav-link\" [routerLink]=\"['/protectedPage']\">Protected Page</a>\n    </li>\n    <li *ngIf=\"isLoggedIn\" class=\"nav-item\" routerLinkActive=\"active\">\n      <a class=\"nav-link\" [routerLink]=\"['/callProtectedApi']\">‍‍Call Protected Api</a>\n    </li>\n    <li *ngIf=\"isLoggedIn\" class=\"nav-item\" routerLinkActive=\"active\">\n      <a class=\"nav-link\" [routerLink]=\"['/changePassword']\">Change Password</a>\n    </li>\n  </ul>\n</nav>\n"

/***/ }),

/***/ "./src/app/core/component/header/header.component.ts":
/*!***********************************************************!*\
  !*** ./src/app/core/component/header/header.component.ts ***!
  \***********************************************************/
/*! exports provided: HeaderComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HeaderComponent", function() { return HeaderComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _services_auth_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../services/auth.service */ "./src/app/core/services/auth.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var HeaderComponent = /** @class */ (function () {
    function HeaderComponent(authService) {
        this.authService = authService;
        this.title = "Angular.Jwt.Core";
        this.isLoggedIn = false;
        this.subscription = null;
        this.displayName = "";
    }
    HeaderComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.subscription = this.authService.authStatus$.subscribe(function (status) {
            _this.isLoggedIn = status;
            if (status) {
                var authUser = _this.authService.getAuthUser();
                _this.displayName = authUser ? authUser.displayName : "";
            }
        });
    };
    HeaderComponent.prototype.ngOnDestroy = function () {
        // prevent memory leak when component is destroyed
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    };
    HeaderComponent.prototype.logout = function () {
        this.authService.logout(true);
    };
    HeaderComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: "app-header",
            template: __webpack_require__(/*! ./header.component.html */ "./src/app/core/component/header/header.component.html"),
            styles: [__webpack_require__(/*! ./header.component.css */ "./src/app/core/component/header/header.component.css")]
        }),
        __metadata("design:paramtypes", [_services_auth_service__WEBPACK_IMPORTED_MODULE_1__["AuthService"]])
    ], HeaderComponent);
    return HeaderComponent;
}());



/***/ }),

/***/ "./src/app/core/core.module.ts":
/*!*************************************!*\
  !*** ./src/app/core/core.module.ts ***!
  \*************************************/
/*! exports provided: CoreModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CoreModule", function() { return CoreModule; });
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _component_header_header_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./component/header/header.component */ "./src/app/core/component/header/header.component.ts");
/* harmony import */ var _services_api_config_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./services/api-config.service */ "./src/app/core/services/api-config.service.ts");
/* harmony import */ var _services_app_config__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./services/app.config */ "./src/app/core/services/app.config.ts");
/* harmony import */ var _services_auth_interceptor__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./services/auth.interceptor */ "./src/app/core/services/auth.interceptor.ts");
/* harmony import */ var _services_xsrf_interceptor__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./services/xsrf.interceptor */ "./src/app/core/services/xsrf.interceptor.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};









var CoreModule = /** @class */ (function () {
    function CoreModule(core) {
        if (core) {
            throw new Error("CoreModule should be imported ONLY in AppModule.");
        }
    }
    CoreModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["NgModule"])({
            imports: [_angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"], _angular_router__WEBPACK_IMPORTED_MODULE_3__["RouterModule"]],
            exports: [
                // components that are used in app.component.ts will be listed here.
                _component_header_header_component__WEBPACK_IMPORTED_MODULE_4__["HeaderComponent"]
            ],
            declarations: [
                // components that are used in app.component.ts will be listed here.
                _component_header_header_component__WEBPACK_IMPORTED_MODULE_4__["HeaderComponent"]
            ],
            providers: [
                /* ``No`` global singleton services of the whole app should be listed here anymore!
                   Since they'll be already provided in AppModule using the `tree-shakable providers` of Angular 6.x+ (providedIn: 'root').
                   This new feature allows cleaning up the providers section from the CoreModule.
                   But if you want to provide something with an InjectionToken other that its class, you still have to use this section.
                */
                {
                    provide: _services_app_config__WEBPACK_IMPORTED_MODULE_6__["APP_CONFIG"],
                    useValue: _services_app_config__WEBPACK_IMPORTED_MODULE_6__["AppConfig"]
                },
                {
                    provide: _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HTTP_INTERCEPTORS"],
                    useClass: _services_xsrf_interceptor__WEBPACK_IMPORTED_MODULE_8__["XsrfInterceptor"],
                    multi: true
                },
                {
                    provide: _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HTTP_INTERCEPTORS"],
                    useClass: _services_auth_interceptor__WEBPACK_IMPORTED_MODULE_7__["AuthInterceptor"],
                    multi: true
                },
                {
                    provide: _angular_core__WEBPACK_IMPORTED_MODULE_2__["APP_INITIALIZER"],
                    useFactory: function (config) { return function () { return config.loadApiConfig(); }; },
                    deps: [_services_api_config_service__WEBPACK_IMPORTED_MODULE_5__["ApiConfigService"]],
                    multi: true
                }
            ]
        }),
        __param(0, Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["Optional"])()), __param(0, Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["SkipSelf"])()),
        __metadata("design:paramtypes", [CoreModule])
    ], CoreModule);
    return CoreModule;
}());



/***/ }),

/***/ "./src/app/core/index.ts":
/*!*******************************!*\
  !*** ./src/app/core/index.ts ***!
  \*******************************/
/*! exports provided: ApiConfigService, APP_CONFIG, AppConfig, TokenStoreService, AuthTokenType, AuthGuard, AuthService, BrowserStorageService, RefreshTokenService, UtilsService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _models_auth_token_type__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./models/auth-token-type */ "./src/app/core/models/auth-token-type.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "AuthTokenType", function() { return _models_auth_token_type__WEBPACK_IMPORTED_MODULE_0__["AuthTokenType"]; });

/* harmony import */ var _services_api_config_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./services/api-config.service */ "./src/app/core/services/api-config.service.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "ApiConfigService", function() { return _services_api_config_service__WEBPACK_IMPORTED_MODULE_1__["ApiConfigService"]; });

/* harmony import */ var _services_app_config__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./services/app.config */ "./src/app/core/services/app.config.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "APP_CONFIG", function() { return _services_app_config__WEBPACK_IMPORTED_MODULE_2__["APP_CONFIG"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "AppConfig", function() { return _services_app_config__WEBPACK_IMPORTED_MODULE_2__["AppConfig"]; });

/* harmony import */ var _services_auth_guard__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./services/auth.guard */ "./src/app/core/services/auth.guard.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "AuthGuard", function() { return _services_auth_guard__WEBPACK_IMPORTED_MODULE_3__["AuthGuard"]; });

/* harmony import */ var _services_auth_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./services/auth.service */ "./src/app/core/services/auth.service.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "AuthService", function() { return _services_auth_service__WEBPACK_IMPORTED_MODULE_4__["AuthService"]; });

/* harmony import */ var _services_browser_storage_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./services/browser-storage.service */ "./src/app/core/services/browser-storage.service.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "BrowserStorageService", function() { return _services_browser_storage_service__WEBPACK_IMPORTED_MODULE_5__["BrowserStorageService"]; });

/* harmony import */ var _services_refresh_token_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./services/refresh-token.service */ "./src/app/core/services/refresh-token.service.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "RefreshTokenService", function() { return _services_refresh_token_service__WEBPACK_IMPORTED_MODULE_6__["RefreshTokenService"]; });

/* harmony import */ var _services_token_store_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./services/token-store.service */ "./src/app/core/services/token-store.service.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "TokenStoreService", function() { return _services_token_store_service__WEBPACK_IMPORTED_MODULE_7__["TokenStoreService"]; });

/* harmony import */ var _services_utils_service__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./services/utils.service */ "./src/app/core/services/utils.service.ts");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "UtilsService", function() { return _services_utils_service__WEBPACK_IMPORTED_MODULE_8__["UtilsService"]; });












/***/ }),

/***/ "./src/app/core/models/auth-token-type.ts":
/*!************************************************!*\
  !*** ./src/app/core/models/auth-token-type.ts ***!
  \************************************************/
/*! exports provided: AuthTokenType */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthTokenType", function() { return AuthTokenType; });
var AuthTokenType;
(function (AuthTokenType) {
    AuthTokenType[AuthTokenType["AccessToken"] = 0] = "AccessToken";
    AuthTokenType[AuthTokenType["RefreshToken"] = 1] = "RefreshToken";
})(AuthTokenType || (AuthTokenType = {}));


/***/ }),

/***/ "./src/app/core/services/api-config.service.ts":
/*!*****************************************************!*\
  !*** ./src/app/core/services/api-config.service.ts ***!
  \*****************************************************/
/*! exports provided: ApiConfigService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ApiConfigService", function() { return ApiConfigService; });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _app_config__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./app.config */ "./src/app/core/services/app.config.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};



var ApiConfigService = /** @class */ (function () {
    function ApiConfigService(injector, appConfig) {
        this.injector = injector;
        this.appConfig = appConfig;
        this.config = null;
    }
    ApiConfigService.prototype.loadApiConfig = function () {
        var _this = this;
        var http = this.injector.get(_angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpClient"]);
        var url = this.appConfig.apiEndpoint + "/" + this.appConfig.apiSettingsPath;
        return http.get(url)
            .toPromise()
            .then(function (config) {
            _this.config = config;
            console.log("ApiConfig", _this.config);
        })
            .catch(function (err) {
            console.error("Failed to loadApiConfig(). Make sure " + url + " is accessible.", _this.config);
            return Promise.reject(err);
        });
    };
    Object.defineProperty(ApiConfigService.prototype, "configuration", {
        get: function () {
            if (!this.config) {
                throw new Error("Attempted to access configuration property before configuration data was loaded.");
            }
            return this.config;
        },
        enumerable: true,
        configurable: true
    });
    ApiConfigService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
            providedIn: 'root'
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Inject"])(_app_config__WEBPACK_IMPORTED_MODULE_2__["APP_CONFIG"])),
        __metadata("design:paramtypes", [_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injector"], Object])
    ], ApiConfigService);
    return ApiConfigService;
}());



/***/ }),

/***/ "./src/app/core/services/app.config.ts":
/*!*********************************************!*\
  !*** ./src/app/core/services/app.config.ts ***!
  \*********************************************/
/*! exports provided: APP_CONFIG, AppConfig */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "APP_CONFIG", function() { return APP_CONFIG; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppConfig", function() { return AppConfig; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");

var APP_CONFIG = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["InjectionToken"]("app.config");
var AppConfig = {
    apiEndpoint: "https://localhost:5001/api",
    apiSettingsPath: "ApiSettings"
};


/***/ }),

/***/ "./src/app/core/services/auth.guard.ts":
/*!*********************************************!*\
  !*** ./src/app/core/services/auth.guard.ts ***!
  \*********************************************/
/*! exports provided: AuthGuard */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthGuard", function() { return AuthGuard; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./auth.service */ "./src/app/core/services/auth.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var AuthGuard = /** @class */ (function () {
    function AuthGuard(authService, router) {
        this.authService = authService;
        this.router = router;
        this.permissionObjectKey = "permission";
    }
    AuthGuard.prototype.canActivate = function (route, state) {
        var permissionData = route.data[this.permissionObjectKey];
        var returnUrl = state.url;
        return this.hasAuthUserAccessToThisRoute(permissionData, returnUrl);
    };
    AuthGuard.prototype.canActivateChild = function (childRoute, state) {
        var permissionData = childRoute.data[this.permissionObjectKey];
        var returnUrl = state.url;
        return this.hasAuthUserAccessToThisRoute(permissionData, returnUrl);
    };
    AuthGuard.prototype.canLoad = function (route) {
        if (route.data) {
            var permissionData = route.data[this.permissionObjectKey];
            var returnUrl = "/" + route.path;
            return this.hasAuthUserAccessToThisRoute(permissionData, returnUrl);
        }
        else {
            return true;
        }
    };
    AuthGuard.prototype.hasAuthUserAccessToThisRoute = function (permissionData, returnUrl) {
        if (!this.authService.isAuthUserLoggedIn()) {
            this.showAccessDenied(returnUrl);
            return false;
        }
        if (!permissionData) {
            return true;
        }
        if (Array.isArray(permissionData.deniedRoles) && Array.isArray(permissionData.permittedRoles)) {
            throw new Error("Don't set both 'deniedRoles' and 'permittedRoles' in route data.");
        }
        if (Array.isArray(permissionData.permittedRoles)) {
            var isInRole = this.authService.isAuthUserInRoles(permissionData.permittedRoles);
            if (isInRole) {
                return true;
            }
            this.showAccessDenied(returnUrl);
            return false;
        }
        if (Array.isArray(permissionData.deniedRoles)) {
            var isInRole = this.authService.isAuthUserInRoles(permissionData.deniedRoles);
            if (!isInRole) {
                return true;
            }
            this.showAccessDenied(returnUrl);
            return false;
        }
        return true;
    };
    AuthGuard.prototype.showAccessDenied = function (returnUrl) {
        this.router.navigate(["/accessDenied"], { queryParams: { returnUrl: returnUrl } });
    };
    AuthGuard = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [_auth_service__WEBPACK_IMPORTED_MODULE_2__["AuthService"], _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]])
    ], AuthGuard);
    return AuthGuard;
}());



/***/ }),

/***/ "./src/app/core/services/auth.interceptor.ts":
/*!***************************************************!*\
  !*** ./src/app/core/services/auth.interceptor.ts ***!
  \***************************************************/
/*! exports provided: AuthInterceptor */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthInterceptor", function() { return AuthInterceptor; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var _models_auth_token_type__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./../models/auth-token-type */ "./src/app/core/models/auth-token-type.ts");
/* harmony import */ var _token_store_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./token-store.service */ "./src/app/core/services/token-store.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var AuthInterceptor = /** @class */ (function () {
    function AuthInterceptor(tokenStoreService, router) {
        this.tokenStoreService = tokenStoreService;
        this.router = router;
        this.delayBetweenRetriesMs = 1000;
        this.numberOfRetries = 3;
        this.authorizationHeader = "Authorization";
    }
    AuthInterceptor.prototype.intercept = function (request, next) {
        var _this = this;
        var accessToken = this.tokenStoreService.getRawAuthToken(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_4__["AuthTokenType"].AccessToken);
        if (accessToken) {
            request = request.clone({
                headers: request.headers.set(this.authorizationHeader, "Bearer " + accessToken)
            });
            return next.handle(request).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["retryWhen"])(function (errors) { return errors.pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["mergeMap"])(function (error, retryAttempt) {
                if (retryAttempt === _this.numberOfRetries - 1) {
                    console.log("HTTP call '" + request.method + " " + request.url + "' failed after " + _this.numberOfRetries + " retries.");
                    return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["throwError"])(error); // no retry
                }
                switch (error.status) {
                    case 400:
                    case 404:
                        return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["throwError"])(error); // no retry
                }
                return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(error); // retry
            }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["delay"])(_this.delayBetweenRetriesMs), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["take"])(_this.numberOfRetries)); }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["catchError"])(function (error, caught) {
                console.error({ error: error, caught: caught });
                if (error.status === 401 || error.status === 403) {
                    var newRequest = _this.getNewAuthRequest(request);
                    if (newRequest) {
                        console.log("Try new AuthRequest ...");
                        return next.handle(newRequest);
                    }
                    _this.router.navigate(["/accessDenied"]);
                }
                return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["throwError"])(error);
            }));
        }
        else {
            // login page
            return next.handle(request);
        }
    };
    AuthInterceptor.prototype.getNewAuthRequest = function (request) {
        var newStoredToken = this.tokenStoreService.getRawAuthToken(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_4__["AuthTokenType"].AccessToken);
        var requestAccessTokenHeader = request.headers.get(this.authorizationHeader);
        if (!newStoredToken || !requestAccessTokenHeader) {
            console.log("There is no new AccessToken.", { requestAccessTokenHeader: requestAccessTokenHeader, newStoredToken: newStoredToken });
            return null;
        }
        var newAccessTokenHeader = "Bearer " + newStoredToken;
        if (requestAccessTokenHeader === newAccessTokenHeader) {
            console.log("There is no new AccessToken.", { requestAccessTokenHeader: requestAccessTokenHeader, newAccessTokenHeader: newAccessTokenHeader });
            return null;
        }
        return request.clone({ headers: request.headers.set(this.authorizationHeader, newAccessTokenHeader) });
    };
    AuthInterceptor = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])(),
        __metadata("design:paramtypes", [_token_store_service__WEBPACK_IMPORTED_MODULE_5__["TokenStoreService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]])
    ], AuthInterceptor);
    return AuthInterceptor;
}());



/***/ }),

/***/ "./src/app/core/services/auth.service.ts":
/*!***********************************************!*\
  !*** ./src/app/core/services/auth.service.ts ***!
  \***********************************************/
/*! exports provided: AuthService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthService", function() { return AuthService; });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var _models_auth_token_type__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./../models/auth-token-type */ "./src/app/core/models/auth-token-type.ts");
/* harmony import */ var _api_config_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./api-config.service */ "./src/app/core/services/api-config.service.ts");
/* harmony import */ var _app_config__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./app.config */ "./src/app/core/services/app.config.ts");
/* harmony import */ var _refresh_token_service__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./refresh-token.service */ "./src/app/core/services/refresh-token.service.ts");
/* harmony import */ var _token_store_service__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./token-store.service */ "./src/app/core/services/token-store.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};










var AuthService = /** @class */ (function () {
    function AuthService(http, router, appConfig, apiConfigService, tokenStoreService, refreshTokenService) {
        this.http = http;
        this.router = router;
        this.appConfig = appConfig;
        this.apiConfigService = apiConfigService;
        this.tokenStoreService = tokenStoreService;
        this.refreshTokenService = refreshTokenService;
        this.authStatusSource = new rxjs__WEBPACK_IMPORTED_MODULE_3__["BehaviorSubject"](false);
        this.authStatus$ = this.authStatusSource.asObservable();
        this.updateStatusOnPageRefresh();
        this.refreshTokenService.scheduleRefreshToken(this.isAuthUserLoggedIn(), false);
    }
    AuthService.prototype.login = function (credentials) {
        var _this = this;
        var headers = new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpHeaders"]({ "Content-Type": "application/json" });
        return this.http
            .post(this.appConfig.apiEndpoint + "/" + this.apiConfigService.configuration.loginPath, credentials, { headers: headers })
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (response) {
            _this.tokenStoreService.setRememberMe(credentials.rememberMe);
            if (!response) {
                console.error("There is no `{'" + _this.apiConfigService.configuration.accessTokenObjectKey +
                    "':'...','" + _this.apiConfigService.configuration.refreshTokenObjectKey + "':'...value...'}` response after login.");
                _this.authStatusSource.next(false);
                return false;
            }
            _this.tokenStoreService.storeLoginSession(response);
            console.log("Logged-in user info", _this.getAuthUser());
            _this.refreshTokenService.scheduleRefreshToken(true, true);
            _this.authStatusSource.next(true);
            return true;
        }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["catchError"])(function (error) { return Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["throwError"])(error); }));
    };
    AuthService.prototype.getBearerAuthHeader = function () {
        return new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpHeaders"]({
            "Content-Type": "application/json",
            "Authorization": "Bearer " + this.tokenStoreService.getRawAuthToken(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_5__["AuthTokenType"].AccessToken)
        });
    };
    AuthService.prototype.logout = function (navigateToHome) {
        var _this = this;
        var headers = new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpHeaders"]({ "Content-Type": "application/json" });
        var refreshToken = encodeURIComponent(this.tokenStoreService.getRawAuthToken(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_5__["AuthTokenType"].RefreshToken));
        this.http
            .get(this.appConfig.apiEndpoint + "/" + this.apiConfigService.configuration.logoutPath + "?refreshToken=" + refreshToken, { headers: headers })
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (response) { return response || {}; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["catchError"])(function (error) { return Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["throwError"])(error); }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["finalize"])(function () {
            _this.tokenStoreService.deleteAuthTokens();
            _this.refreshTokenService.unscheduleRefreshToken(true);
            _this.authStatusSource.next(false);
            if (navigateToHome) {
                _this.router.navigate(["/"]);
            }
        }))
            .subscribe(function (result) {
            console.log("logout", result);
        });
    };
    AuthService.prototype.isAuthUserLoggedIn = function () {
        return this.tokenStoreService.hasStoredAccessAndRefreshTokens() &&
            !this.tokenStoreService.isAccessTokenTokenExpired();
    };
    AuthService.prototype.getAuthUser = function () {
        if (!this.isAuthUserLoggedIn()) {
            return null;
        }
        var decodedToken = this.tokenStoreService.getDecodedAccessToken();
        var roles = this.tokenStoreService.getDecodedTokenRoles();
        return Object.freeze({
            userId: decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
            userName: decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
            displayName: decodedToken["DisplayName"],
            roles: roles
        });
    };
    AuthService.prototype.isAuthUserInRoles = function (requiredRoles) {
        var user = this.getAuthUser();
        if (!user || !user.roles) {
            return false;
        }
        if (user.roles.indexOf(this.apiConfigService.configuration.adminRoleName.toLowerCase()) >= 0) {
            return true; // The `Admin` role has full access to every pages.
        }
        return requiredRoles.some(function (requiredRole) {
            if (user.roles) {
                return user.roles.indexOf(requiredRole.toLowerCase()) >= 0;
            }
            else {
                return false;
            }
        });
    };
    AuthService.prototype.isAuthUserInRole = function (requiredRole) {
        return this.isAuthUserInRoles([requiredRole]);
    };
    AuthService.prototype.updateStatusOnPageRefresh = function () {
        this.authStatusSource.next(this.isAuthUserLoggedIn());
    };
    AuthService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
            providedIn: 'root'
        }),
        __param(2, Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Inject"])(_app_config__WEBPACK_IMPORTED_MODULE_7__["APP_CONFIG"])),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpClient"],
            _angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"], Object, _api_config_service__WEBPACK_IMPORTED_MODULE_6__["ApiConfigService"],
            _token_store_service__WEBPACK_IMPORTED_MODULE_9__["TokenStoreService"],
            _refresh_token_service__WEBPACK_IMPORTED_MODULE_8__["RefreshTokenService"]])
    ], AuthService);
    return AuthService;
}());



/***/ }),

/***/ "./src/app/core/services/browser-storage.service.ts":
/*!**********************************************************!*\
  !*** ./src/app/core/services/browser-storage.service.ts ***!
  \**********************************************************/
/*! exports provided: BrowserStorageService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BrowserStorageService", function() { return BrowserStorageService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var BrowserStorageService = /** @class */ (function () {
    function BrowserStorageService() {
    }
    BrowserStorageService.prototype.getSession = function (key) {
        var data = window.sessionStorage.getItem(key);
        if (data) {
            return JSON.parse(data);
        }
        else {
            return null;
        }
    };
    BrowserStorageService.prototype.setSession = function (key, value) {
        var data = value === undefined ? "" : JSON.stringify(value);
        window.sessionStorage.setItem(key, data);
    };
    BrowserStorageService.prototype.removeSession = function (key) {
        window.sessionStorage.removeItem(key);
    };
    BrowserStorageService.prototype.removeAllSessions = function () {
        for (var key in window.sessionStorage) {
            if (window.sessionStorage.hasOwnProperty(key)) {
                this.removeSession(key);
            }
        }
    };
    BrowserStorageService.prototype.getLocal = function (key) {
        var data = window.localStorage.getItem(key);
        if (data) {
            return JSON.parse(data);
        }
        else {
            return null;
        }
    };
    BrowserStorageService.prototype.setLocal = function (key, value) {
        var data = value === undefined ? "" : JSON.stringify(value);
        window.localStorage.setItem(key, data);
    };
    BrowserStorageService.prototype.removeLocal = function (key) {
        window.localStorage.removeItem(key);
    };
    BrowserStorageService.prototype.removeAllLocals = function () {
        for (var key in window.localStorage) {
            if (window.localStorage.hasOwnProperty(key)) {
                this.removeLocal(key);
            }
        }
    };
    BrowserStorageService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        })
    ], BrowserStorageService);
    return BrowserStorageService;
}());



/***/ }),

/***/ "./src/app/core/services/refresh-token.service.ts":
/*!********************************************************!*\
  !*** ./src/app/core/services/refresh-token.service.ts ***!
  \********************************************************/
/*! exports provided: RefreshTokenService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "RefreshTokenService", function() { return RefreshTokenService; });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var _models_auth_token_type__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./../models/auth-token-type */ "./src/app/core/models/auth-token-type.ts");
/* harmony import */ var _api_config_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./api-config.service */ "./src/app/core/services/api-config.service.ts");
/* harmony import */ var _app_config__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./app.config */ "./src/app/core/services/app.config.ts");
/* harmony import */ var _browser_storage_service__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./browser-storage.service */ "./src/app/core/services/browser-storage.service.ts");
/* harmony import */ var _token_store_service__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./token-store.service */ "./src/app/core/services/token-store.service.ts");
/* harmony import */ var _utils_service__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./utils.service */ "./src/app/core/services/utils.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};










var RefreshTokenService = /** @class */ (function () {
    function RefreshTokenService(tokenStoreService, appConfig, apiConfigService, http, browserStorageService, utilsService) {
        this.tokenStoreService = tokenStoreService;
        this.appConfig = appConfig;
        this.apiConfigService = apiConfigService;
        this.http = http;
        this.browserStorageService = browserStorageService;
        this.utilsService = utilsService;
        this.refreshTokenTimerCheckId = "is_refreshToken_timer_started";
        this.refreshTokenSubscription = null;
    }
    RefreshTokenService.prototype.scheduleRefreshToken = function (isAuthUserLoggedIn, calledFromLogin) {
        var _this = this;
        this.unscheduleRefreshToken(false);
        if (!isAuthUserLoggedIn) {
            return;
        }
        var expDateUtc = this.tokenStoreService.getAccessTokenExpirationDateUtc();
        if (!expDateUtc) {
            throw new Error("This access token has not the `exp` property.");
        }
        var expiresAtUtc = expDateUtc.valueOf();
        var nowUtc = new Date().valueOf();
        var threeSeconds = 3000;
        // threeSeconds instead of 1 to prevent other tab timers run less than threeSeconds
        var initialDelay = Math.max(threeSeconds, expiresAtUtc - nowUtc - threeSeconds);
        console.log("Initial scheduleRefreshToken Delay(ms)", initialDelay);
        var timerSource$ = Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["timer"])(initialDelay);
        this.refreshTokenSubscription = timerSource$.subscribe(function () {
            if (calledFromLogin || !_this.isRefreshTokenTimerStartedInAnotherTab()) {
                _this.refreshToken(isAuthUserLoggedIn);
            }
            else {
                _this.scheduleRefreshToken(isAuthUserLoggedIn, false);
            }
        });
        if (calledFromLogin || !this.isRefreshTokenTimerStartedInAnotherTab()) {
            this.setRefreshTokenTimerStarted();
        }
    };
    RefreshTokenService.prototype.unscheduleRefreshToken = function (cancelTimerCheckToken) {
        if (this.refreshTokenSubscription) {
            this.refreshTokenSubscription.unsubscribe();
        }
        if (cancelTimerCheckToken) {
            this.deleteRefreshTokenTimerCheckId();
        }
    };
    RefreshTokenService.prototype.invalidateCurrentTabId = function () {
        if (!this.tokenStoreService.rememberMe()) {
            return;
        }
        var currentTabId = this.utilsService.getCurrentTabId();
        var timerStat = this.browserStorageService.getLocal(this.refreshTokenTimerCheckId);
        if (timerStat && timerStat.tabId === currentTabId) {
            this.setRefreshTokenTimerStopped();
        }
    };
    RefreshTokenService.prototype.refreshToken = function (isAuthUserLoggedIn) {
        var _this = this;
        var headers = new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpHeaders"]({ "Content-Type": "application/json" });
        var model = { refreshToken: this.tokenStoreService.getRawAuthToken(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_4__["AuthTokenType"].RefreshToken) };
        return this.http
            .post(this.appConfig.apiEndpoint + "/" + this.apiConfigService.configuration.refreshTokenPath, model, { headers: headers })
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["map"])(function (response) { return response || {}; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["catchError"])(function (error) { return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["throwError"])(error); }))
            .subscribe(function (result) {
            console.log("RefreshToken Result", result);
            _this.tokenStoreService.storeLoginSession(result);
            // this.deleteRefreshTokenTimerCheckId();
            _this.scheduleRefreshToken(isAuthUserLoggedIn, false);
        });
    };
    RefreshTokenService.prototype.isRefreshTokenTimerStartedInAnotherTab = function () {
        if (!this.tokenStoreService.rememberMe()) {
            return false; // It uses the session storage for the tokens and its access scope is limited to the current tab.
        }
        var currentTabId = this.utilsService.getCurrentTabId();
        var timerStat = this.browserStorageService.getLocal(this.refreshTokenTimerCheckId);
        console.log("RefreshTokenTimer Check", {
            refreshTokenTimerCheckId: timerStat,
            currentTabId: currentTabId
        });
        var isStarted = timerStat && timerStat.isStarted === true && timerStat.tabId !== currentTabId;
        if (isStarted) {
            console.log("RefreshToken timer has already been started in another tab with tabId=" + timerStat.tabId + ".\n      currentTabId=" + currentTabId + ".");
        }
        return isStarted;
    };
    RefreshTokenService.prototype.setRefreshTokenTimerStarted = function () {
        this.browserStorageService.setLocal(this.refreshTokenTimerCheckId, {
            isStarted: true,
            tabId: this.utilsService.getCurrentTabId()
        });
    };
    RefreshTokenService.prototype.deleteRefreshTokenTimerCheckId = function () {
        this.browserStorageService.removeLocal(this.refreshTokenTimerCheckId);
    };
    RefreshTokenService.prototype.setRefreshTokenTimerStopped = function () {
        this.browserStorageService.setLocal(this.refreshTokenTimerCheckId, {
            isStarted: false,
            tabId: -1
        });
    };
    RefreshTokenService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
            providedIn: 'root'
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Inject"])(_app_config__WEBPACK_IMPORTED_MODULE_6__["APP_CONFIG"])),
        __metadata("design:paramtypes", [_token_store_service__WEBPACK_IMPORTED_MODULE_8__["TokenStoreService"], Object, _api_config_service__WEBPACK_IMPORTED_MODULE_5__["ApiConfigService"],
            _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpClient"],
            _browser_storage_service__WEBPACK_IMPORTED_MODULE_7__["BrowserStorageService"],
            _utils_service__WEBPACK_IMPORTED_MODULE_9__["UtilsService"]])
    ], RefreshTokenService);
    return RefreshTokenService;
}());



/***/ }),

/***/ "./src/app/core/services/token-store.service.ts":
/*!******************************************************!*\
  !*** ./src/app/core/services/token-store.service.ts ***!
  \******************************************************/
/*! exports provided: TokenStoreService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TokenStoreService", function() { return TokenStoreService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var jwt_decode__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! jwt-decode */ "./node_modules/jwt-decode/lib/index.js");
/* harmony import */ var jwt_decode__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(jwt_decode__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./../models/auth-token-type */ "./src/app/core/models/auth-token-type.ts");
/* harmony import */ var _api_config_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./api-config.service */ "./src/app/core/services/api-config.service.ts");
/* harmony import */ var _browser_storage_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./browser-storage.service */ "./src/app/core/services/browser-storage.service.ts");
/* harmony import */ var _utils_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./utils.service */ "./src/app/core/services/utils.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var TokenStoreService = /** @class */ (function () {
    function TokenStoreService(browserStorageService, utilsService, apiConfigService) {
        this.browserStorageService = browserStorageService;
        this.utilsService = utilsService;
        this.apiConfigService = apiConfigService;
        this.rememberMeToken = "rememberMe_token";
    }
    TokenStoreService.prototype.getRawAuthToken = function (tokenType) {
        if (this.rememberMe()) {
            return this.browserStorageService.getLocal(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"][tokenType]);
        }
        else {
            return this.browserStorageService.getSession(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"][tokenType]);
        }
    };
    TokenStoreService.prototype.getDecodedAccessToken = function () {
        return jwt_decode__WEBPACK_IMPORTED_MODULE_1__(this.getRawAuthToken(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"].AccessToken));
    };
    TokenStoreService.prototype.getAuthUserDisplayName = function () {
        return this.getDecodedAccessToken().DisplayName;
    };
    TokenStoreService.prototype.getAccessTokenExpirationDateUtc = function () {
        var decoded = this.getDecodedAccessToken();
        if (decoded.exp === undefined) {
            return null;
        }
        var date = new Date(0); // The 0 sets the date to the epoch
        date.setUTCSeconds(decoded.exp);
        return date;
    };
    TokenStoreService.prototype.isAccessTokenTokenExpired = function () {
        var expirationDateUtc = this.getAccessTokenExpirationDateUtc();
        if (!expirationDateUtc) {
            return true;
        }
        return !(expirationDateUtc.valueOf() > new Date().valueOf());
    };
    TokenStoreService.prototype.deleteAuthTokens = function () {
        if (this.rememberMe()) {
            this.browserStorageService.removeLocal(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"][_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"].AccessToken]);
            this.browserStorageService.removeLocal(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"][_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"].RefreshToken]);
        }
        else {
            this.browserStorageService.removeSession(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"][_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"].AccessToken]);
            this.browserStorageService.removeSession(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"][_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"].RefreshToken]);
        }
        this.browserStorageService.removeLocal(this.rememberMeToken);
    };
    TokenStoreService.prototype.setToken = function (tokenType, tokenValue) {
        if (this.utilsService.isEmptyString(tokenValue)) {
            console.error(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"][tokenType] + " is null or empty.");
        }
        if (tokenType === _models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"].AccessToken && this.utilsService.isEmptyString(tokenValue)) {
            throw new Error("AccessToken can't be null or empty.");
        }
        if (this.rememberMe()) {
            this.browserStorageService.setLocal(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"][tokenType], tokenValue);
        }
        else {
            this.browserStorageService.setSession(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"][tokenType], tokenValue);
        }
    };
    TokenStoreService.prototype.getDecodedTokenRoles = function () {
        var decodedToken = this.getDecodedAccessToken();
        var roles = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        if (!roles) {
            return null;
        }
        if (Array.isArray(roles)) {
            return roles.map(function (role) { return role.toLowerCase(); });
        }
        else {
            return [roles.toLowerCase()];
        }
    };
    TokenStoreService.prototype.storeLoginSession = function (response) {
        this.setToken(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"].AccessToken, response[this.apiConfigService.configuration.accessTokenObjectKey]);
        this.setToken(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"].RefreshToken, response[this.apiConfigService.configuration.refreshTokenObjectKey]);
    };
    TokenStoreService.prototype.rememberMe = function () {
        return this.browserStorageService.getLocal(this.rememberMeToken) === true;
    };
    TokenStoreService.prototype.setRememberMe = function (value) {
        this.browserStorageService.setLocal(this.rememberMeToken, value);
    };
    TokenStoreService.prototype.hasStoredAccessAndRefreshTokens = function () {
        var accessToken = this.getRawAuthToken(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"].AccessToken);
        var refreshToken = this.getRawAuthToken(_models_auth_token_type__WEBPACK_IMPORTED_MODULE_2__["AuthTokenType"].RefreshToken);
        return !this.utilsService.isEmptyString(accessToken) && !this.utilsService.isEmptyString(refreshToken);
    };
    TokenStoreService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [_browser_storage_service__WEBPACK_IMPORTED_MODULE_4__["BrowserStorageService"],
            _utils_service__WEBPACK_IMPORTED_MODULE_5__["UtilsService"],
            _api_config_service__WEBPACK_IMPORTED_MODULE_3__["ApiConfigService"]])
    ], TokenStoreService);
    return TokenStoreService;
}());



/***/ }),

/***/ "./src/app/core/services/utils.service.ts":
/*!************************************************!*\
  !*** ./src/app/core/services/utils.service.ts ***!
  \************************************************/
/*! exports provided: UtilsService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "UtilsService", function() { return UtilsService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _browser_storage_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./browser-storage.service */ "./src/app/core/services/browser-storage.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var UtilsService = /** @class */ (function () {
    function UtilsService(browserStorageService) {
        this.browserStorageService = browserStorageService;
    }
    UtilsService.prototype.isEmptyString = function (value) {
        return !value || 0 === value.length;
    };
    UtilsService.prototype.getCurrentTabId = function () {
        var tabIdToken = "currentTabId";
        var tabId = this.browserStorageService.getSession(tabIdToken);
        if (tabId) {
            return tabId;
        }
        tabId = Math.random();
        this.browserStorageService.setSession(tabIdToken, tabId);
        return tabId;
    };
    UtilsService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [_browser_storage_service__WEBPACK_IMPORTED_MODULE_1__["BrowserStorageService"]])
    ], UtilsService);
    return UtilsService;
}());



/***/ }),

/***/ "./src/app/core/services/xsrf.interceptor.ts":
/*!***************************************************!*\
  !*** ./src/app/core/services/xsrf.interceptor.ts ***!
  \***************************************************/
/*! exports provided: XsrfInterceptor */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "XsrfInterceptor", function() { return XsrfInterceptor; });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var XsrfInterceptor = /** @class */ (function () {
    function XsrfInterceptor(tokenExtractor) {
        this.tokenExtractor = tokenExtractor;
    }
    XsrfInterceptor.prototype.intercept = function (request, next) {
        /*
            const lcUrl = request.url.toLowerCase();
            if (request.method === "GET" || request.method === "HEAD" ||
              lcUrl.startsWith("http://") || lcUrl.startsWith("https://")) {
              console.log("Original HttpXsrfInterceptor skips both non-mutating requests and absolute URLs.");
              console.log("Skipped request", { lcUrl: lcUrl, method: request.method });
            }
        */
        if (request.method === "POST") {
            var headerName = "X-XSRF-TOKEN";
            var token = this.tokenExtractor.getToken();
            if (token && !request.headers.has(headerName)) {
                request = request.clone({
                    headers: request.headers.set(headerName, token)
                });
            }
        }
        return next.handle(request);
    };
    XsrfInterceptor = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])(),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpXsrfTokenExtractor"]])
    ], XsrfInterceptor);
    return XsrfInterceptor;
}());



/***/ }),

/***/ "./src/app/dashboard/call-protected-api/call-protected-api.component.css":
/*!*******************************************************************************!*\
  !*** ./src/app/dashboard/call-protected-api/call-protected-api.component.css ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/dashboard/call-protected-api/call-protected-api.component.html":
/*!********************************************************************************!*\
  !*** ./src/app/dashboard/call-protected-api/call-protected-api.component.html ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"card\">\n  <div class=\"card-header\">\n    <h2 class=\"card-title\">Call protected API</h2>\n  </div>\n  <div class=\"card-body\">\n    <button *ngIf=\"isAdmin\" class=\"btn btn-primary\" (click)=\"callMyProtectedAdminApiController()\">\n      Call [Authorize(Roles = \"Admin\")] API\n    </button>\n\n    <button *ngIf=\"isAdmin || isUser\" class=\"btn btn-default\" (click)=\"callMyProtectedApiController()\">\n      Call [Authorize] API\n    </button>\n\n    <button class=\"btn btn-warning\" (click)=\"callMyProtectedEditorsApiController()\">\n      Call [Authorize(Roles = \"Editor\")] API\n    </button>\n\n    <div *ngIf=\"result\" class=\"highlight\">\n      <pre><code>{{result | json}}</code></pre>\n    </div>\n  </div>\n</div>\n\n<div class=\"top15\">\n  <div class=\"card\">\n    <div class=\"card-header\">\n      <h2 class=\"card-title\">Show/Hide elements using isVisibleForAuthUser directive</h2>\n    </div>\n    <div class=\"card-body\">\n      <div class=\"alert alert-info\" isVisibleForAuthUser>\n        Is-Visible-For-AuthUser\n      </div>\n      <div class=\"alert alert-success\" isVisibleForAuthUser [isVisibleForRoles]=\"['Admin','User']\">\n        Is-Visible-For-Roles = ['Admin','User']\n      </div>\n    </div>\n  </div>\n</div>\n\n<div class=\"top15\">\n  <div class=\"card\">\n    <div class=\"card-header\">\n      <h2 class=\"card-title\">Show/Hide elements using *hasAuthUserViewPermission directive</h2>\n    </div>\n    <div class=\"card-body\">\n      <div class=\"alert alert-info\" *hasAuthUserViewPermission=\"\">\n        *hasAuthUserViewPermission=\"\"\n      </div>\n      <div class=\"alert alert-success\" *hasAuthUserViewPermission=\"['Admin','User']\">\n        *hasAuthUserViewPermission=\"['Admin','User']\"\n      </div>\n    </div>\n  </div>\n</div>\n"

/***/ }),

/***/ "./src/app/dashboard/call-protected-api/call-protected-api.component.ts":
/*!******************************************************************************!*\
  !*** ./src/app/dashboard/call-protected-api/call-protected-api.component.ts ***!
  \******************************************************************************/
/*! exports provided: CallProtectedApiComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CallProtectedApiComponent", function() { return CallProtectedApiComponent; });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _app_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @app/core */ "./src/app/core/index.ts");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var CallProtectedApiComponent = /** @class */ (function () {
    function CallProtectedApiComponent(authService, httpClient, appConfig) {
        this.authService = authService;
        this.httpClient = httpClient;
        this.appConfig = appConfig;
        this.isAdmin = false;
        this.isUser = false;
    }
    CallProtectedApiComponent.prototype.ngOnInit = function () {
        this.isAdmin = this.authService.isAuthUserInRole("Admin");
        this.isUser = this.authService.isAuthUserInRole("User");
    };
    CallProtectedApiComponent.prototype.callMyProtectedAdminApiController = function () {
        var _this = this;
        this.httpClient
            .get(this.appConfig.apiEndpoint + "/MyProtectedAdminApi")
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (response) { return response || {}; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["catchError"])(function (error) { return Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["throwError"])(error); }))
            .subscribe(function (result) {
            _this.result = result;
        });
    };
    CallProtectedApiComponent.prototype.callMyProtectedApiController = function () {
        var _this = this;
        this.httpClient
            .get(this.appConfig.apiEndpoint + "/MyProtectedApi")
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (response) { return response || {}; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["catchError"])(function (error) { return Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["throwError"])(error); }))
            .subscribe(function (result) {
            _this.result = result;
        });
    };
    CallProtectedApiComponent.prototype.callMyProtectedEditorsApiController = function () {
        var _this = this;
        this.httpClient
            .get(this.appConfig.apiEndpoint + "/MyProtectedEditorsApi")
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (response) { return response || {}; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["catchError"])(function (error) { return Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["throwError"])(error); }))
            .subscribe(function (result) {
            _this.result = result;
        });
    };
    CallProtectedApiComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
            selector: "app-call-protected-api",
            template: __webpack_require__(/*! ./call-protected-api.component.html */ "./src/app/dashboard/call-protected-api/call-protected-api.component.html"),
            styles: [__webpack_require__(/*! ./call-protected-api.component.css */ "./src/app/dashboard/call-protected-api/call-protected-api.component.css")]
        }),
        __param(2, Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Inject"])(_app_core__WEBPACK_IMPORTED_MODULE_2__["APP_CONFIG"])),
        __metadata("design:paramtypes", [_app_core__WEBPACK_IMPORTED_MODULE_2__["AuthService"],
            _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpClient"], Object])
    ], CallProtectedApiComponent);
    return CallProtectedApiComponent;
}());



/***/ }),

/***/ "./src/app/dashboard/dashboard-routing.module.ts":
/*!*******************************************************!*\
  !*** ./src/app/dashboard/dashboard-routing.module.ts ***!
  \*******************************************************/
/*! exports provided: DashboardRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DashboardRoutingModule", function() { return DashboardRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _app_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @app/core */ "./src/app/core/index.ts");
/* harmony import */ var _call_protected_api_call_protected_api_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./call-protected-api/call-protected-api.component */ "./src/app/dashboard/call-protected-api/call-protected-api.component.ts");
/* harmony import */ var _protected_page_protected_page_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./protected-page/protected-page.component */ "./src/app/dashboard/protected-page/protected-page.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};





var routes = [
    {
        path: "protectedPage",
        component: _protected_page_protected_page_component__WEBPACK_IMPORTED_MODULE_4__["ProtectedPageComponent"],
        data: {
            permission: {
                permittedRoles: ["Admin"]
            }
        },
        canActivate: [_app_core__WEBPACK_IMPORTED_MODULE_2__["AuthGuard"]]
    },
    {
        path: "callProtectedApi",
        component: _call_protected_api_call_protected_api_component__WEBPACK_IMPORTED_MODULE_3__["CallProtectedApiComponent"],
        data: {
            permission: {
                permittedRoles: ["Admin", "User"]
            }
        },
        canActivate: [_app_core__WEBPACK_IMPORTED_MODULE_2__["AuthGuard"]]
    }
];
var DashboardRoutingModule = /** @class */ (function () {
    function DashboardRoutingModule() {
    }
    DashboardRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], DashboardRoutingModule);
    return DashboardRoutingModule;
}());



/***/ }),

/***/ "./src/app/dashboard/dashboard.module.ts":
/*!***********************************************!*\
  !*** ./src/app/dashboard/dashboard.module.ts ***!
  \***********************************************/
/*! exports provided: DashboardModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DashboardModule", function() { return DashboardModule; });
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _shared_shared_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../shared/shared.module */ "./src/app/shared/shared.module.ts");
/* harmony import */ var _call_protected_api_call_protected_api_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./call-protected-api/call-protected-api.component */ "./src/app/dashboard/call-protected-api/call-protected-api.component.ts");
/* harmony import */ var _dashboard_routing_module__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./dashboard-routing.module */ "./src/app/dashboard/dashboard-routing.module.ts");
/* harmony import */ var _protected_page_protected_page_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./protected-page/protected-page.component */ "./src/app/dashboard/protected-page/protected-page.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






var DashboardModule = /** @class */ (function () {
    function DashboardModule() {
    }
    DashboardModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"],
                _shared_shared_module__WEBPACK_IMPORTED_MODULE_2__["SharedModule"],
                _dashboard_routing_module__WEBPACK_IMPORTED_MODULE_4__["DashboardRoutingModule"]
            ],
            declarations: [_protected_page_protected_page_component__WEBPACK_IMPORTED_MODULE_5__["ProtectedPageComponent"], _call_protected_api_call_protected_api_component__WEBPACK_IMPORTED_MODULE_3__["CallProtectedApiComponent"]]
        })
    ], DashboardModule);
    return DashboardModule;
}());



/***/ }),

/***/ "./src/app/dashboard/protected-page/protected-page.component.css":
/*!***********************************************************************!*\
  !*** ./src/app/dashboard/protected-page/protected-page.component.css ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/dashboard/protected-page/protected-page.component.html":
/*!************************************************************************!*\
  !*** ./src/app/dashboard/protected-page/protected-page.component.html ***!
  \************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h1>\n  Decoded Access Token\n</h1>\n\n<div class=\"alert alert-info\">\n  <label> Access Token Expiration Date:</label> {{accessTokenExpirationDate}}\n</div>\n\n<div class=\"highlight\">\n  <pre><code>{{decodedAccessToken | json}}</code></pre>\n</div>\n"

/***/ }),

/***/ "./src/app/dashboard/protected-page/protected-page.component.ts":
/*!**********************************************************************!*\
  !*** ./src/app/dashboard/protected-page/protected-page.component.ts ***!
  \**********************************************************************/
/*! exports provided: ProtectedPageComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ProtectedPageComponent", function() { return ProtectedPageComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _app_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @app/core */ "./src/app/core/index.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var ProtectedPageComponent = /** @class */ (function () {
    function ProtectedPageComponent(tokenStoreService) {
        this.tokenStoreService = tokenStoreService;
        this.decodedAccessToken = {};
        this.accessTokenExpirationDate = null;
    }
    ProtectedPageComponent.prototype.ngOnInit = function () {
        this.decodedAccessToken = this.tokenStoreService.getDecodedAccessToken();
        this.accessTokenExpirationDate = this.tokenStoreService.getAccessTokenExpirationDateUtc();
    };
    ProtectedPageComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: "app-protected-page",
            template: __webpack_require__(/*! ./protected-page.component.html */ "./src/app/dashboard/protected-page/protected-page.component.html"),
            styles: [__webpack_require__(/*! ./protected-page.component.css */ "./src/app/dashboard/protected-page/protected-page.component.css")]
        }),
        __metadata("design:paramtypes", [_app_core__WEBPACK_IMPORTED_MODULE_1__["TokenStoreService"]])
    ], ProtectedPageComponent);
    return ProtectedPageComponent;
}());



/***/ }),

/***/ "./src/app/page-not-found/page-not-found.component.css":
/*!*************************************************************!*\
  !*** ./src/app/page-not-found/page-not-found.component.css ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/page-not-found/page-not-found.component.html":
/*!**************************************************************!*\
  !*** ./src/app/page-not-found/page-not-found.component.html ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<p>\n  page-not-found works!\n</p>\n"

/***/ }),

/***/ "./src/app/page-not-found/page-not-found.component.ts":
/*!************************************************************!*\
  !*** ./src/app/page-not-found/page-not-found.component.ts ***!
  \************************************************************/
/*! exports provided: PageNotFoundComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PageNotFoundComponent", function() { return PageNotFoundComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var PageNotFoundComponent = /** @class */ (function () {
    function PageNotFoundComponent() {
    }
    PageNotFoundComponent.prototype.ngOnInit = function () {
    };
    PageNotFoundComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: "app-page-not-found",
            template: __webpack_require__(/*! ./page-not-found.component.html */ "./src/app/page-not-found/page-not-found.component.html"),
            styles: [__webpack_require__(/*! ./page-not-found.component.css */ "./src/app/page-not-found/page-not-found.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], PageNotFoundComponent);
    return PageNotFoundComponent;
}());



/***/ }),

/***/ "./src/app/request/add/add.component.css":
/*!***********************************************!*\
  !*** ./src/app/request/add/add.component.css ***!
  \***********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/request/add/add.component.html":
/*!************************************************!*\
  !*** ./src/app/request/add/add.component.html ***!
  \************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "\r\n<div class=\"card\">\r\n  <div class=\"card-header\">\r\n    <h2 class=\"card-title\">درخواست جدید</h2>\r\n  </div>\r\n  <div class=\"card-body\">\r\n    <div class=\"container\">\r\n      <form #form=\"ngForm\" (submit)=\"submitForm(form)\" novalidate>\r\n        <div class=\"form-group\">\r\n          <label class=\"control-label\">عنوان </label>\r\n          <input name=\"title\" #title=\"ngModel\" [class.is-invalid]=\"title.invalid && title.touched\"\r\n                 type=\"text\" required class=\"form-control\" name=\"title\" [(ngModel)]=\"model.title\">\r\n          <ng-container *ngTemplateOutlet=\"validationErrorsTemplate; context:{ control: title }\"></ng-container>\r\n        </div>\r\n        <div class=\"form-group\">\r\n          <label class=\"control-label\">متن درخواست</label>\r\n          <input name=\"body\" #body=\"ngModel\" [class.is-invalid]=\"body.invalid && body.touched\"\r\n                 type=\"text\" required minlength=\"4\" class=\"form-control\" name=\"body\"\r\n                 appValidateEqual compare-to=\"confirmPassword\" [(ngModel)]=\"model.newPassword\">\r\n          <ng-container *ngTemplateOutlet=\"validationErrorsTemplate; context:{ control: body}\"></ng-container>\r\n        </div>\r\n\r\n\r\n        <button class=\"btn btn-primary\" [disabled]=\"form.invalid\" type=\"submit\">ارسال</button>\r\n        <div *ngIf=\"error\" class=\"alert alert-danger \" role=\"alert \">\r\n          {{error}}\r\n        </div>\r\n      </form>\r\n    </div>\r\n  </div>\r\n</div>\r\n<ng-template #validationErrorsTemplate let-ctrl=\"control\">\r\n  <div *ngIf=\"ctrl.invalid && ctrl.touched\">\r\n    <div class=\"alert alert-danger\" *ngIf=\"ctrl.errors.required\">\r\n      This field is required.\r\n    </div>\r\n    <div class=\"alert alert-danger\" *ngIf=\"ctrl.errors.minlength\">\r\n      This field should be minimum {{ctrl.errors.minlength.requiredLength}} characters.\r\n    </div>\r\n    <div class=\"alert alert-danger\" *ngIf=\"ctrl.errors.maxlength\">\r\n      This field should be max {{ctrl.errors.maxlength.requiredLength}} characters.\r\n    </div>\r\n    <div class=\"alert alert-danger\" *ngIf=\"ctrl.errors.pattern\">\r\n      This field's pattern: {{ctrl.errors.pattern.requiredPattern}}\r\n    </div>\r\n    <div class=\"alert alert-danger\" *ngIf=\"ctrl.errors.modelStateError\">\r\n      {{ctrl.errors.modelStateError.error}}\r\n    </div>\r\n    <div class=\"alert alert-danger\" *ngIf=\"ctrl.errors.appValidateEqual\">\r\n      Password mismatch.\r\n    </div>\r\n  </div>\r\n</ng-template>\r\n"

/***/ }),

/***/ "./src/app/request/add/add.component.ts":
/*!**********************************************!*\
  !*** ./src/app/request/add/add.component.ts ***!
  \**********************************************/
/*! exports provided: AddComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AddComponent", function() { return AddComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_request_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./../services/request.service */ "./src/app/request/services/request.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var AddComponent = /** @class */ (function () {
    function AddComponent(router, requestService) {
        this.router = router;
        this.requestService = requestService;
        this.error = "";
        this.model = {
            id: 0,
            title: "",
            processId: 0,
            dateRequested: "",
            userId: 0,
            currentStateId: 0,
            body: ""
        };
    }
    AddComponent.prototype.ngOnInit = function () { };
    AddComponent.prototype.submitForm = function (form) {
        var _this = this;
        console.log(this.model);
        console.log(form.value);
        this.model.processId = 2;
        this.requestService.add(this.model)
            .subscribe(function () {
            _this.router.navigate(["/welcome"]);
        }, function (error) {
            console.error("ChangePassword error", error);
            _this.error = error.error + " -> " + error.statusText + ": " + error.message;
        });
    };
    AddComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-add',
            template: __webpack_require__(/*! ./add.component.html */ "./src/app/request/add/add.component.html"),
            styles: [__webpack_require__(/*! ./add.component.css */ "./src/app/request/add/add.component.css")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"], _services_request_service__WEBPACK_IMPORTED_MODULE_2__["RequestService"]])
    ], AddComponent);
    return AddComponent;
}());



/***/ }),

/***/ "./src/app/request/request-routing.module.ts":
/*!***************************************************!*\
  !*** ./src/app/request/request-routing.module.ts ***!
  \***************************************************/
/*! exports provided: RequestRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "RequestRoutingModule", function() { return RequestRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _add_add_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./add/add.component */ "./src/app/request/add/add.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



var routes = [
    { path: "add", component: _add_add_component__WEBPACK_IMPORTED_MODULE_2__["AddComponent"] }
];
var RequestRoutingModule = /** @class */ (function () {
    function RequestRoutingModule() {
    }
    RequestRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], RequestRoutingModule);
    return RequestRoutingModule;
}());



/***/ }),

/***/ "./src/app/request/request.module.ts":
/*!*******************************************!*\
  !*** ./src/app/request/request.module.ts ***!
  \*******************************************/
/*! exports provided: RequestModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "RequestModule", function() { return RequestModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _request_routing_module__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./request-routing.module */ "./src/app/request/request-routing.module.ts");
/* harmony import */ var _add_add_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./add/add.component */ "./src/app/request/add/add.component.ts");
/* harmony import */ var _services_request_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./services/request.service */ "./src/app/request/services/request.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






var RequestModule = /** @class */ (function () {
    function RequestModule() {
    }
    RequestModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_1__["CommonModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"],
                _request_routing_module__WEBPACK_IMPORTED_MODULE_3__["RequestRoutingModule"]
            ],
            declarations: [_add_add_component__WEBPACK_IMPORTED_MODULE_4__["AddComponent"]],
            providers: [_services_request_service__WEBPACK_IMPORTED_MODULE_5__["RequestService"]]
        })
    ], RequestModule);
    return RequestModule;
}());



/***/ }),

/***/ "./src/app/request/services/request.service.ts":
/*!*****************************************************!*\
  !*** ./src/app/request/services/request.service.ts ***!
  \*****************************************************/
/*! exports provided: RequestService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "RequestService", function() { return RequestService; });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _app_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @app/core */ "./src/app/core/index.ts");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};





var RequestService = /** @class */ (function () {
    function RequestService(http, appConfig) {
        this.http = http;
        this.appConfig = appConfig;
    }
    RequestService.prototype.add = function (model) {
        var headers = new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpHeaders"]({ "Content-Type": "application/json" });
        var url = this.appConfig.apiEndpoint + "/request/add";
        return this.http
            .post(url, model, { headers: headers })
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (response) { return response || {}; }), Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["catchError"])(function (error) { return Object(rxjs__WEBPACK_IMPORTED_MODULE_3__["throwError"])(error); }));
    };
    RequestService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])(),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Inject"])(_app_core__WEBPACK_IMPORTED_MODULE_2__["APP_CONFIG"])),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpClient"], Object])
    ], RequestService);
    return RequestService;
}());



/***/ }),

/***/ "./src/app/shared/directives/equal-validator.directive.ts":
/*!****************************************************************!*\
  !*** ./src/app/shared/directives/equal-validator.directive.ts ***!
  \****************************************************************/
/*! exports provided: EqualValidatorDirective */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EqualValidatorDirective", function() { return EqualValidatorDirective; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};


var EqualValidatorDirective = /** @class */ (function () {
    function EqualValidatorDirective(compareToControl) {
        this.compareToControl = compareToControl;
    }
    EqualValidatorDirective_1 = EqualValidatorDirective;
    EqualValidatorDirective.prototype.validate = function (element) {
        var selfValue = element.value;
        var otherControl = element.root.get(this.compareToControl);
        /*
        console.log("EqualValidatorDirective", {
          thisControlValue: selfValue,
          otherControlValue: otherControl ? otherControl.value : null
        });
        */
        if (otherControl && selfValue !== otherControl.value) {
            return {
                appValidateEqual: true // Or a string such as 'Password mismatch.' or an abject.
            };
        }
        if (otherControl &&
            otherControl.errors &&
            selfValue === otherControl.value) {
            delete otherControl.errors["appValidateEqual"];
            if (!Object.keys(otherControl.errors).length) {
                otherControl.setErrors(null);
            }
        }
        return null;
    };
    EqualValidatorDirective = EqualValidatorDirective_1 = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"])({
            // https://angular.io/guide/styleguide#style-02-08
            // Do use a custom prefix for the selector of directives (e.g, the prefix toh from Tour of Heroes).
            // Do spell non-element selectors in lower camel case unless the selector is meant to match a native HTML attribute.
            // the directive matches elements that have the attribute appValidateEqual and one of the formControlName or formControl or ngModel
            selector: "[appValidateEqual][formControlName],[appValidateEqual][formControl],[appValidateEqual][ngModel]",
            providers: [
                {
                    provide: _angular_forms__WEBPACK_IMPORTED_MODULE_1__["NG_VALIDATORS"],
                    useExisting: EqualValidatorDirective_1,
                    multi: true // the new directives are added to the previously registered directives instead of overriding them.
                }
            ]
        }),
        __param(0, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Attribute"])("compare-to")),
        __metadata("design:paramtypes", [String])
    ], EqualValidatorDirective);
    return EqualValidatorDirective;
    var EqualValidatorDirective_1;
}());



/***/ }),

/***/ "./src/app/shared/directives/has-auth-user-view-permission.directive.ts":
/*!******************************************************************************!*\
  !*** ./src/app/shared/directives/has-auth-user-view-permission.directive.ts ***!
  \******************************************************************************/
/*! exports provided: HasAuthUserViewPermissionDirective */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HasAuthUserViewPermissionDirective", function() { return HasAuthUserViewPermissionDirective; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _app_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @app/core */ "./src/app/core/index.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var HasAuthUserViewPermissionDirective = /** @class */ (function () {
    // Note, if you don't place the * in front, you won't be able to inject the TemplateRef<any> or ViewContainerRef into your directive.
    function HasAuthUserViewPermissionDirective(templateRef, viewContainer, authService) {
        this.templateRef = templateRef;
        this.viewContainer = viewContainer;
        this.authService = authService;
        this.isVisible = false;
        this.requiredRoles = null;
        this.subscription = null;
    }
    Object.defineProperty(HasAuthUserViewPermissionDirective.prototype, "hasAuthUserViewPermission", {
        set: function (roles) {
            this.requiredRoles = roles;
        },
        enumerable: true,
        configurable: true
    });
    HasAuthUserViewPermissionDirective.prototype.ngOnInit = function () {
        var _this = this;
        this.subscription = this.authService.authStatus$.subscribe(function (status) { return _this.changeVisibility(status); });
        this.changeVisibility(this.authService.isAuthUserLoggedIn());
    };
    HasAuthUserViewPermissionDirective.prototype.ngOnDestroy = function () {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    };
    HasAuthUserViewPermissionDirective.prototype.changeVisibility = function (status) {
        var isInRoles = !this.requiredRoles ? true : this.authService.isAuthUserInRoles(this.requiredRoles);
        if (isInRoles && status) {
            if (!this.isVisible) {
                this.viewContainer.createEmbeddedView(this.templateRef);
                this.isVisible = true;
            }
        }
        else {
            this.isVisible = false;
            this.viewContainer.clear();
        }
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object),
        __metadata("design:paramtypes", [Object])
    ], HasAuthUserViewPermissionDirective.prototype, "hasAuthUserViewPermission", null);
    HasAuthUserViewPermissionDirective = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"])({
            selector: "[hasAuthUserViewPermission]"
        }),
        __metadata("design:paramtypes", [_angular_core__WEBPACK_IMPORTED_MODULE_0__["TemplateRef"],
            _angular_core__WEBPACK_IMPORTED_MODULE_0__["ViewContainerRef"],
            _app_core__WEBPACK_IMPORTED_MODULE_1__["AuthService"]])
    ], HasAuthUserViewPermissionDirective);
    return HasAuthUserViewPermissionDirective;
}());



/***/ }),

/***/ "./src/app/shared/directives/is-visible-for-auth-user.directive.ts":
/*!*************************************************************************!*\
  !*** ./src/app/shared/directives/is-visible-for-auth-user.directive.ts ***!
  \*************************************************************************/
/*! exports provided: IsVisibleForAuthUserDirective */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "IsVisibleForAuthUserDirective", function() { return IsVisibleForAuthUserDirective; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _app_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @app/core */ "./src/app/core/index.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var IsVisibleForAuthUserDirective = /** @class */ (function () {
    function IsVisibleForAuthUserDirective(elem, authService) {
        this.elem = elem;
        this.authService = authService;
        this.subscription = null;
        this.isVisibleForRoles = null;
    }
    IsVisibleForAuthUserDirective.prototype.ngOnDestroy = function () {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    };
    IsVisibleForAuthUserDirective.prototype.ngOnInit = function () {
        var _this = this;
        this.subscription = this.authService.authStatus$.subscribe(function (status) { return _this.changeVisibility(status); });
        this.changeVisibility(this.authService.isAuthUserLoggedIn());
    };
    IsVisibleForAuthUserDirective.prototype.changeVisibility = function (status) {
        var isInRoles = !this.isVisibleForRoles ? true : this.authService.isAuthUserInRoles(this.isVisibleForRoles);
        this.elem.nativeElement.style.display = isInRoles && status ? "" : "none";
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Object)
    ], IsVisibleForAuthUserDirective.prototype, "isVisibleForRoles", void 0);
    IsVisibleForAuthUserDirective = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Directive"])({
            selector: "[isVisibleForAuthUser]"
        }),
        __metadata("design:paramtypes", [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"], _app_core__WEBPACK_IMPORTED_MODULE_1__["AuthService"]])
    ], IsVisibleForAuthUserDirective);
    return IsVisibleForAuthUserDirective;
}());



/***/ }),

/***/ "./src/app/shared/shared.module.ts":
/*!*****************************************!*\
  !*** ./src/app/shared/shared.module.ts ***!
  \*****************************************/
/*! exports provided: SharedModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SharedModule", function() { return SharedModule; });
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _directives_equal_validator_directive__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./directives/equal-validator.directive */ "./src/app/shared/directives/equal-validator.directive.ts");
/* harmony import */ var _directives_has_auth_user_view_permission_directive__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./directives/has-auth-user-view-permission.directive */ "./src/app/shared/directives/has-auth-user-view-permission.directive.ts");
/* harmony import */ var _directives_is_visible_for_auth_user_directive__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./directives/is-visible-for-auth-user.directive */ "./src/app/shared/directives/is-visible-for-auth-user.directive.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};







var SharedModule = /** @class */ (function () {
    function SharedModule() {
    }
    SharedModule_1 = SharedModule;
    SharedModule.forRoot = function () {
        // Forcing the whole app to use the returned providers from the AppModule only.
        return {
            ngModule: SharedModule_1,
            providers: []
        };
    };
    SharedModule = SharedModule_1 = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_2__["NgModule"])({
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormsModule"],
                _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClientModule"]
            ],
            entryComponents: [],
            declarations: [
                // common and shared components/directives/pipes between more than one module and components will be listed here.
                _directives_is_visible_for_auth_user_directive__WEBPACK_IMPORTED_MODULE_6__["IsVisibleForAuthUserDirective"],
                _directives_has_auth_user_view_permission_directive__WEBPACK_IMPORTED_MODULE_5__["HasAuthUserViewPermissionDirective"],
                _directives_equal_validator_directive__WEBPACK_IMPORTED_MODULE_4__["EqualValidatorDirective"]
            ],
            exports: [
                // common and shared components/directives/pipes between more than one module and components will be listed here.
                _angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_3__["FormsModule"],
                _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClientModule"],
                _directives_is_visible_for_auth_user_directive__WEBPACK_IMPORTED_MODULE_6__["IsVisibleForAuthUserDirective"],
                _directives_has_auth_user_view_permission_directive__WEBPACK_IMPORTED_MODULE_5__["HasAuthUserViewPermissionDirective"],
                _directives_equal_validator_directive__WEBPACK_IMPORTED_MODULE_4__["EqualValidatorDirective"]
            ]
            /* No providers here! Since they’ll be already provided in AppModule. */
        })
    ], SharedModule);
    return SharedModule;
    var SharedModule_1;
}());



/***/ }),

/***/ "./src/app/welcome/welcome.component.css":
/*!***********************************************!*\
  !*** ./src/app/welcome/welcome.component.css ***!
  \***********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/welcome/welcome.component.html":
/*!************************************************!*\
  !*** ./src/app/welcome/welcome.component.html ***!
  \************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div style=\"text-align:center\">\n  <h1>\n    Welcome!\n  </h1>\n  <img width=\"300\" src=\"data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz4NCjwhLS0gR2VuZXJhdG9yOiBBZG9iZSBJbGx1c3RyYXRvciAxOS4xLjAsIFNWRyBFeHBvcnQgUGx1Zy1JbiAuIFNWRyBWZXJzaW9uOiA2LjAwIEJ1aWxkIDApICAtLT4NCjxzdmcgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgeD0iMHB4IiB5PSIwcHgiDQoJIHZpZXdCb3g9IjAgMCAyNTAgMjUwIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCAyNTAgMjUwOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSI+DQo8c3R5bGUgdHlwZT0idGV4dC9jc3MiPg0KCS5zdDB7ZmlsbDojREQwMDMxO30NCgkuc3Qxe2ZpbGw6I0MzMDAyRjt9DQoJLnN0MntmaWxsOiNGRkZGRkY7fQ0KPC9zdHlsZT4NCjxnPg0KCTxwb2x5Z29uIGNsYXNzPSJzdDAiIHBvaW50cz0iMTI1LDMwIDEyNSwzMCAxMjUsMzAgMzEuOSw2My4yIDQ2LjEsMTg2LjMgMTI1LDIzMCAxMjUsMjMwIDEyNSwyMzAgMjAzLjksMTg2LjMgMjE4LjEsNjMuMiAJIi8+DQoJPHBvbHlnb24gY2xhc3M9InN0MSIgcG9pbnRzPSIxMjUsMzAgMTI1LDUyLjIgMTI1LDUyLjEgMTI1LDE1My40IDEyNSwxNTMuNCAxMjUsMjMwIDEyNSwyMzAgMjAzLjksMTg2LjMgMjE4LjEsNjMuMiAxMjUsMzAgCSIvPg0KCTxwYXRoIGNsYXNzPSJzdDIiIGQ9Ik0xMjUsNTIuMUw2Ni44LDE4Mi42aDBoMjEuN2gwbDExLjctMjkuMmg0OS40bDExLjcsMjkuMmgwaDIxLjdoMEwxMjUsNTIuMUwxMjUsNTIuMUwxMjUsNTIuMUwxMjUsNTIuMQ0KCQlMMTI1LDUyLjF6IE0xNDIsMTM1LjRIMTA4bDE3LTQwLjlMMTQyLDEzNS40eiIvPg0KPC9nPg0KPC9zdmc+DQo=\"\n  />\n</div>\n"

/***/ }),

/***/ "./src/app/welcome/welcome.component.ts":
/*!**********************************************!*\
  !*** ./src/app/welcome/welcome.component.ts ***!
  \**********************************************/
/*! exports provided: WelcomeComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "WelcomeComponent", function() { return WelcomeComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var WelcomeComponent = /** @class */ (function () {
    function WelcomeComponent() {
    }
    WelcomeComponent.prototype.ngOnInit = function () {
    };
    WelcomeComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: "app-welcome",
            template: __webpack_require__(/*! ./welcome.component.html */ "./src/app/welcome/welcome.component.html"),
            styles: [__webpack_require__(/*! ./welcome.component.css */ "./src/app/welcome/welcome.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], WelcomeComponent);
    return WelcomeComponent;
}());



/***/ }),

/***/ "./src/environments/environment.ts":
/*!*****************************************!*\
  !*** ./src/environments/environment.ts ***!
  \*****************************************/
/*! exports provided: environment */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "environment", function() { return environment; });
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
var environment = {
    production: false
};


/***/ }),

/***/ "./src/main.ts":
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser-dynamic */ "./node_modules/@angular/platform-browser-dynamic/fesm5/platform-browser-dynamic.js");
/* harmony import */ var _app_app_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./app/app.module */ "./src/app/app.module.ts");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./environments/environment */ "./src/environments/environment.ts");




if (_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].production) {
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["enableProdMode"])();
}
Object(_angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__["platformBrowserDynamic"])().bootstrapModule(_app_app_module__WEBPACK_IMPORTED_MODULE_2__["AppModule"])
    .catch(function (err) { return console.error(err); });


/***/ }),

/***/ 0:
/*!***************************!*\
  !*** multi ./src/main.ts ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! D:\DNT\src\ASPNETCore2JwtAuthentication.AngularClient\src\main.ts */"./src/main.ts");


/***/ })

},[[0,"runtime","vendor"]]]);
//# sourceMappingURL=main.js.map