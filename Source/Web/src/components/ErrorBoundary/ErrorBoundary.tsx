import React, { Component, ErrorInfo } from 'react';

interface ErrorBoundaryProps {
  children: React.ReactNode;
}

interface ErrorBoundaryState {
  hasError: boolean;
}

class ErrorBoundary extends Component<ErrorBoundaryProps, ErrorBoundaryState> {
  constructor(props: ErrorBoundaryProps) {
    super(props);
    this.state = {
      hasError: false
    };
  }

  static getDerivedStateFromError(error: Error) {
    // Update state so the next render will show the fallback UI.
    return { hasError: true };
  }

  componentDidCatch(error: Error, errorInfo: ErrorInfo) {
    // You can also log the error to an error reporting service
    console.error('Error occurred in the application:', error, errorInfo);
  }

  render() {
    if (this.state.hasError) {
      // You can render any custom fallback UI
      return <h1>Something went wrong.</h1>;
    }

    return this.props.children;
  }
}

export default ErrorBoundary;






// import React, { Component } from 'react';
// import ErrorPage from '../../pages/ErrorPage/ErrorPage';
// class ErrorBoundary extends Component {
//   constructor(props) {
//     super(props);
//     this.state = { hasError: false };
//   }

//   componentDidCatch(error, info) {
//     this.setState({ hasError: true });
//     // You can also log the error to an error reporting service
//     console.error('Error Boundary:', error, info);
//   }

//   render() {
//     if (this.state.hasError) {
//       // Render the error page component
//       return <ErrorPage />;
//     }
//     return this.props.children;
//   }
// }

// export default ErrorBoundary;
