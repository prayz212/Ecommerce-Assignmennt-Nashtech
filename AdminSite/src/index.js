// @ts-nocheck
import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { Provider } from 'react-redux';
import store from './services/store';
import { BrowserRouter as Router } from 'react-router-dom';
import history from './config/react-router';

ReactDOM.render(
  <Provider store={store}>
      <Router history={history}>
        <App />
      </Router>
  </Provider>,
  document.getElementById('root')
);