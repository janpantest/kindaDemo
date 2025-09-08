import { defineConfig } from '@playwright/test';

export default defineConfig({
  testDir: './tests/BE',
  use: {
    baseURL: 'https://demoqa.com',
  },
  retries: 0,
  reporter: 'html',
  projects: [
    {
      name: 'api',
      use: {
        // baseURL: 'https://demoqa.com',
      },
    }
  ]
});
