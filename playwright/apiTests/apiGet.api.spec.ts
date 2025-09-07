import { test, expect, request, APIRequestContext, APIResponse } from '@playwright/test';
import dotenv from 'dotenv';

test.describe('GET API call', () => {
  let apiContext: APIRequestContext;
  const url = process.env.BASE_URL!;


  dotenv.config();

  test.beforeAll(async () => {
    apiContext = await request.newContext({
      baseURL: url,
      extraHTTPHeaders: {
        'Content-Type': 'application/json',
      },
    });
  });

  test('GET request', async () => {
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
