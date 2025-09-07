import { test, expect, request, APIRequestContext, APIResponse } from '@playwright/test';

test.describe('Chained API calls - DemoQA', () => {
  let apiContext: APIRequestContext;

  const baseURL = 'https://demoqa.com';
  const userName = `test_${Date.now()}`;
  const password = '1testTEST!';
  let userId: string;
  let token: string;

  test.beforeAll(async () => {
    apiContext = await request.newContext({
      baseURL,
      extraHTTPHeaders: {
        'Content-Type': 'application/json',
        accept: 'application/json',
      },
    });
  });

  test('Create user -> Generate token -> Authorize -> Add book', async () => {
    // Create user
    const createUserResponse = await apiContext.post('/Account/v1/User', {
      data: { userName, password },
    });
    expect(createUserResponse.status()).toBe(201);
    const createUserBody = await createUserResponse.json();
    // console.info('Create User Response:', createUserBody);
    userId = createUserBody.userID;
    expect(userId).toBeTruthy();

    // Generate token
    const tokenResponse = await apiContext.post('/Account/v1/GenerateToken', {
      data: { userName, password },
    });
    expect(tokenResponse.status()).toBe(200);
    const tokenBody = await tokenResponse.json();
    // console.info('Generate Token Response:', tokenBody);
    token = tokenBody.token;
    expect(token).toBeTruthy();

    // Authorize user
    const authResponse = await apiContext.post('/Account/v1/Authorized', {
      data: { userName, password },
    });
    expect(authResponse.status()).toBe(200);
    const authResult = await authResponse.json();
    // console.info('Authorize Response:', authResult);

    // Add book
    const addBookResponse = await apiContext.post('/BookStore/v1/Books', {
      headers: { Authorization: `Bearer ${token}` },
      data: {
        userId,
        collectionOfIsbns: [{ isbn: '9781449325862' }],
      },
    });
    expect(addBookResponse.status()).toBe(201);
    const addBookBody = await addBookResponse.json();
    // console.info('Add Book Response:', addBookBody);
  });

  test.afterAll(async () => {
    await apiContext.dispose();
  });
});
