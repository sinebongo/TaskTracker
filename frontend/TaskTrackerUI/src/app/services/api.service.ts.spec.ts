import { ApiServiceTs } from './api.service.ts';
import { HttpClient } from '@angular/common/http';

describe('ApiServiceTs', () => {
  it('should create an instance', () => {
    const httpClientMock = {} as HttpClient;
    expect(new ApiServiceTs(httpClientMock)).toBeTruthy();
  });
});
