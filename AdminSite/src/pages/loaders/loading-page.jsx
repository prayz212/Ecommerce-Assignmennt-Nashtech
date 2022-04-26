import Loader from "../../components/loader/loader";

const LoadingPage = () => {
  return (
    <div className="flex h-screen">
      <div className="m-auto">
        <Loader />
      </div>
    </div>
  );
};

export default LoadingPage;