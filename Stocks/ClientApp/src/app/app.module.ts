import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { GlobalUserManager } from './global-user-manager';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { AboutComponent } from './about/about.component';
import { UserStocksComponent } from './user-stocks/user-stocks.component';
import { StockItemsComponent } from './stock-items/stock-items.component';
import { ItemHistoryComponent } from './item-history/item-history.component';
import { ItemStateComponent } from './item-state/item-state.component';
import { AddStockComponent } from './add-stock/add-stock.component';
import { AddItemComponent } from './add-item/add-item.component';
import { AddStateComponent } from './add-state/add-state.component';
import { MoveItemComponent } from './move-item/move-item.component';
import { AdministratingComponent } from './administrating/administrating.component';
import { AdmUsersComponent } from './adm-users/adm-users.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    ProfileComponent,
    EditUserComponent,
    AboutComponent,
    UserStocksComponent,
    StockItemsComponent,
    ItemHistoryComponent,
    ItemStateComponent,
    AddStockComponent,
    AddItemComponent,
    AddStateComponent,
    MoveItemComponent,
    AdministratingComponent,
    AdmUsersComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'edit-user', component: EditUserComponent },
      { path: 'about', component: AboutComponent },
      { path: 'user-stocks', component: UserStocksComponent },
      { path: 'stock-items/:id', component: StockItemsComponent },
      { path: 'item-history/:id', component: ItemHistoryComponent },
      { path: 'item-state/:id', component: ItemStateComponent },
      { path: 'add-stock', component: AddStockComponent },
      { path: 'add-item/:id', component: AddItemComponent },
      { path: 'add-state/:id', component: AddStateComponent },
      { path: 'move-item/:id', component: MoveItemComponent },
      { path: 'administrating', component: AdministratingComponent },
      { path: 'adm-users', component: AdmUsersComponent },
    ])
  ],
  providers: [GlobalUserManager],
  bootstrap: [AppComponent]
})
export class AppModule { }
