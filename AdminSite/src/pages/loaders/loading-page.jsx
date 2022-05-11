import Loader from "../../components/loader/loader";
import { DEFAULT_LOADING_PAGE_BACKGROUND_COLOR, DEFAULT_LOADING_PAGE_OPACITY } from "../../constants/variables";

const LoadingPage = ({ backgroundColor = DEFAULT_LOADING_PAGE_BACKGROUND_COLOR, opacityValue = DEFAULT_LOADING_PAGE_OPACITY }) => {
  return (
    <div className={`flex h-screen fixed top-0 right-0 left-0 ${backgroundColor} opacity-${opacityValue}`}>
      <div className="m-auto">
        <Loader />
      </div>
    </div>
  );
};

export default LoadingPage;