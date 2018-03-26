BulletHit = SimpleClass(BaseHit)

function BulletHit:__init(...)

end 

function BulletHit:onExecute() 
    local bdata = BulletHelper:getData(self.cfg.mapId)
    local player = EntityMgr:getEntity(self.roleId)
    if player then 
	    bdata.startPos = player.root.transform.position
	    bdata.goalUID = 800000010002
	    bdata.callBack = function(bid,eid,pos)
	        LuaExtend:loadObj(bdata.expPath,function(obj)  
	          LuaExtend:setObjPos(obj,pos.x,pos.y,pos.z)
	        end)
	    end
	    LuaExtend:createBullet(bdata)
    end
end 