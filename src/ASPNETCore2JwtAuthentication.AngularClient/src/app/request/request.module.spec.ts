import { RequestModule } from './request.module';

describe('RequestModule', () => {
  let requestModule: RequestModule;

  beforeEach(() => {
    requestModule = new RequestModule();
  });

  it('should create an instance', () => {
    expect(requestModule).toBeTruthy();
  });
});
