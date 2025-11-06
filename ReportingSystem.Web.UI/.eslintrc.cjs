/* eslint-env node */
module.exports = {
  root: true,
  env: { browser: true, es2020: true },
  extends: [
    'eslint:recommended',
    'plugin:@typescript-eslint/recommended',
    'plugin:react-hooks/recommended',
    'plugin:jsx-a11y/recommended',
    'plugin:prettier/recommended',
  ],
  ignorePatterns: ['dist', '.eslintrc.cjs', 'vite.config.ts', 'playwright.config.ts'],
  parser: '@typescript-eslint/parser',
  plugins: ['react-refresh', '@typescript-eslint', 'jsx-a11y', 'prettier'],
  rules: {
    'prettier/prettier': 'warn',
    'react-refresh/only-export-components': [
      'warn',
      { allowConstantExport: true },
    ],
    '@typescript-eslint/no-unused-vars': [
      'warn',
      { argsIgnorePattern: '^_', varsIgnorePattern: '^_' },
    ],
    '@typescript-eslint/no-explicit-any': 'warn',
    'no-console': 'warn',
    // From US-115 (WCAG 2.1 AA Compliance)
    'jsx-a11y/anchor-is-valid': [
        'error',
        {
            components: ['Link'],
            specialLink: ['to'],
        },
    ],
  },
  settings: {
    react: {
      version: 'detect',
    },
  },
};