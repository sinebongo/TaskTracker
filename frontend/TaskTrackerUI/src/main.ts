import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { provideHttpClient } from '@angular/common/http';

  const configWithHttp = {
    ...appConfig,
    providers: [
      ...(appConfig.providers || []),
      provideHttpClient()
    ]
  };

  bootstrapApplication(App, configWithHttp)
    .catch((err) => console.error(err));