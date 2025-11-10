import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';

  const configWithHttp = {
    ...appConfig,
    providers: [
      ...(appConfig.providers || []),
      provideHttpClient(),
      provideRouter(routes)
    ]
  };

  bootstrapApplication(App, configWithHttp)
    .catch((err) => console.error(err));