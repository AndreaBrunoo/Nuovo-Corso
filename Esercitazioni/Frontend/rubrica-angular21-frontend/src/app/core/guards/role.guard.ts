import { inject } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateFn, Router } from "@angular/router";
import { AuthService } from "../../services/auth.service";

export const roleGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const roles = (route.data["roles"] as string[] | undefined) ?? [];

  if (roles.length === 0 || authService.hasAnyRole(roles)) {
    console.log('USER:', authService.currentUser());
    console.log('ROLES REQUESTED:', roles);
    console.log('HAS ROLE:', authService.hasAnyRole(roles))
    return true;
  }

  return router.createUrlTree(["/dashboard"]);
};