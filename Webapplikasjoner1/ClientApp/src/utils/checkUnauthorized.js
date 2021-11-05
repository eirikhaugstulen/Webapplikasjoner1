import history from "../history";

export const checkUnauthorized = (e) => {
    if (e?.response?.status === 401) {
        history.push('/admin/logginn')
    }
} 