import { test, expect, request, APIRequestContext, APIResponse } from '@playwright/test';

test.describe('API Testing with Playwright + TypeScript', () => {
  let apiContext: APIRequestContext;
  const url = 'https://demoqa.com';


  test.beforeAll(async ({ playwright }) => {
    apiContext = await request.newContext({
      baseURL: url,
      extraHTTPHeaders: {
        'Content-Type': 'application/json',
      },
    });
  });

  test('GET request example', async () => {
    const response: APIResponse = await apiContext.get('/BookStore/v1/Books');
    expect(response.status()).toBe(200);

    const body: { books: any[] } = await response.json();
    // console.info(body);

    expect(Array.isArray(body.books)).toBe(true);
    expect(body.books.length).toBeGreaterThan(0);
    
    expect(body).toHaveProperty('books');
    expect(Array.isArray(body.books)).toBe(true);
    expect(body.books.length).toBeGreaterThan(0);

    body.books.forEach(book => {
      expect(book).toHaveProperty('isbn');
      expect(book).toHaveProperty('title');
      expect(book).toHaveProperty('author');
      expect(typeof book.isbn).toBe('string');
      expect(typeof book.title).toBe('string');
      expect(typeof book.author).toBe('string');
    });
  });

  test.afterAll(async () => {
    await apiContext.dispose();
  });
});
